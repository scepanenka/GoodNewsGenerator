using System;
using System.Threading.Tasks;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Queries.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(Guid id)
        {
            try
            {
                var article = await _mediator.Send(new GetArticleById(id));
                if (article == null)
                {
                    return NotFound();
                }

                return Ok(article);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}