using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace GoodNews.BL.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsParser _newsParser;
        private const string url = @"https://onliner.by/feed";

        public NewsController(IUnitOfWork unitOfWork, INewsParser newsParser)
        {
            _unitOfWork = unitOfWork;
            _newsParser = newsParser;
        }

        public IActionResult Index()
        {
            var news = _newsParser.GetFromUrl(url);
            _newsParser.AddRange(news);
            _unitOfWork.Save();

            return View(news);
        }
    }
}