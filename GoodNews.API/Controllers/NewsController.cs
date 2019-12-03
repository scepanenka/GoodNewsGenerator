using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoodNews.API.Models;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Queries.GetArticleById;
using GoodNews.MediatR.Queries.GetNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetAllNews()
        {
            IEnumerable<Article> news = await _mediator.Send(new GetNews());
            var newsDto = _mapper.Map<IEnumerable<ArticleDTO>>(news);
            return newsDto;
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