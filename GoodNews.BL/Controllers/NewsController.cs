using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Services.Parsers;

namespace GoodNews.BL.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsParser _onlinerParser;
        private readonly INewsParser _s13Parser;

        public NewsController(IUnitOfWork unitOfWork, IS13Parser s13Parser, IOnlinerParser onlinerParser)
        {
            _unitOfWork = unitOfWork;
            _onlinerParser = onlinerParser;
            _s13Parser = s13Parser;
        }

        public IActionResult Index()
        {


            var news = _unitOfWork.News.GetAll().OrderByDescending(x => x.DateOfPublication);
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
