using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Parsers;

namespace GoodNews.BL.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsParser _onlinerParser;
        private readonly INewsParser _s13Parser;
        private readonly INewsParser _tutByParser;

        public NewsController(IUnitOfWork unitOfWork, IS13Parser s13Parser, IOnlinerParser onlinerParser, ITutByParser tutByParser)
        {
            _unitOfWork = unitOfWork;
            _onlinerParser = onlinerParser;
            _s13Parser = s13Parser;
            _tutByParser = tutByParser;
        }

        public IActionResult Index()
        {


            var news = _unitOfWork.News.AsQueryable().Include(article => article.Source)
                                                     .OrderByDescending(article => article.DateOfPublication);
            return View(news);
        }

        public IActionResult News()
        {
            var news = _unitOfWork.News.GetAll();
            return PartialView(news);
        }

        public IActionResult Parse()
        {
            _onlinerParser.Parse();
            _s13Parser.Parse();
            _tutByParser.Parse();


            return RedirectToAction("Index", "News");
            ;
        }

        [HttpGet]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = _unitOfWork.News.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
}
