using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.DAL;
using GoodNews.Data;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddComment;
using GoodNews.MediatR.Queries.GetArticleById;
using GoodNews.MediatR.Queries.GetCommentsByArticleId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serilog;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public CommentsController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: api/Comments/5
        /// <summary>
        /// Get comments by article id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> Get(Guid articleId)
        {
            try
            {
                IEnumerable<Comment> comments = await _mediator.Send(new GetCommentsByArticleId(articleId));


                Log.Debug($"Comments loaded");

                if (comments.Any())
                {
                    return Ok(comments);
                }
                return StatusCode(204, "No comments");
            }
            catch (Exception e)
            {
                Log.Error($"Comments was not loaded: {e.Message}");

                return StatusCode(500);
            }
        }

        // POST: api/Comments
        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CommentPostModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var comment = new Comment()
                {
                    User = user,
                    Content = model.Content,
                    Date = DateTime.Now.ToLocalTime(),
                    Article = await _mediator.Send(new GetArticleById(model.ArticleId))
                };

                await _mediator.Send(new AddComment(comment));

                Log.Information($"Comment added successfully");

                return StatusCode(201, "Comment added successfully");
            }
            catch (Exception ex)
            {
                Log.Error($"Error adding comment:{Environment.NewLine}{ex.Message}");
                return BadRequest();
            }
        }
    }
}