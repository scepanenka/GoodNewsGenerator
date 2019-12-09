using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.DAL;
using GoodNews.Data;
using GoodNews.Data.Entities;
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

                Log.Information("Article loaded");
                return Ok(articleModel);
            }
            catch (Exception e)
            {
                Log.Error($"Article was not loaded: {e.Message}");

                return StatusCode(500);
            }
        }

        // POST: api/Comments
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
