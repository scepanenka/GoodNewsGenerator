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
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(Guid articleId)
        {
            try
            {
                var article = await _mediator.Send(new GetArticleById(articleId));
                var comments = await _mediator.Send(new GetCommentsByArticleId(articleId));

                var articleModel = new ArticleDetailPageViewModel()
                {
                    Article = article,
                    Comments = comments
                };

                Log.Debug($"Article '{article.Title}'  loaded");
                return Ok(articleModel);
            }
            catch (Exception e)
            {
                Log.Error($"Article was not loaded: {e.Message}");

                return StatusCode(500);
            }
        }

        // POST: api/Comments
        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string input, Guid id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

                var comment = new Comment()
                {
                    User = user,
                    Content = input,
                    Date = DateTime.Now.ToLocalTime(),
                    Article = await _mediator.Send(new GetArticleById(id))
                };

                await _mediator.Send(new AddComment(comment));

                Log.Information($"Comment was added successfully");

                return StatusCode(201, comment);
            }
            catch (Exception ex)
            {
                Log.Error($"Error adding comment:{Environment.NewLine}{ex.Message}");
                return BadRequest();
            }

        }
    }
}
