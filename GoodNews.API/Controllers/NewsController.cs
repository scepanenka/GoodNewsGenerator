using System;
using System.Threading.Tasks;
using GoodNews.Core;
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
        private readonly INewsParser _newsParser;


        public NewsController(IMediator mediator, INewsParser newsParser)
        {
            _mediator = mediator;
            _newsParser = newsParser;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(Guid id)
        {
            await _newsParser.Parse("http://s13.ru/rss");
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