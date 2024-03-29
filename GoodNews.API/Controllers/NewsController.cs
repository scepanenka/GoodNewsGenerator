﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoodNews.API.Models;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Queries.GetArticleById;
using GoodNews.MediatR.Queries.GetNewsByPage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GoodNews.API.Controllers
{
    [Route("[controller]/[action]")]
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

        /// <summary>
        /// Get news by page number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleNewsPageViewModel>>> GetNews(int page=1)
        {
            try
            {
                IEnumerable<Article> news = await _mediator.Send(new GetNewsByPage(page));
                var newsDto = _mapper.Map<IEnumerable<ArticleNewsPageViewModel>>(news);
                Log.Information("News received from database");
                return Ok(newsDto);
            }
            catch (Exception e)
            {

                Log.Error($"Error receiving news from database: {Environment.NewLine}{e.Message}");
                return StatusCode(500, "ERROR RECEIVING NEWS");
            }
            
        }

        /// <summary>
        /// Get article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                Log.Information($"Article '{article.Title}' received from database");
                return Ok(article);
            }
            catch (Exception e)
            {
                Log.Error($"Error receiving article from database: {Environment.NewLine}{e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}