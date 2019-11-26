using System;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MVC.ViewModels;
using GoodNews.MvcServices.ParsersUoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly INewsParser _onlinerParser;
        private readonly INewsParser _s13Parser;
        private readonly INewsParser _tutByParser;
        const string ONLINER = @"https://people.onliner.by/feed";
        const string TUTBY = @"https://news.tut.by/rss/all.rss";
        const string S13 = @"http://s13.ru/rss";

        public NewsController(IUnitOfWork unitOfWork,
                                UserManager<User> userManager,
                                IS13Parser s13Parser,
                                IOnlinerParser onlinerParser, 
                                ITutByParser tutByParser)
        {
            _unitOfWork = unitOfWork;
            _onlinerParser = onlinerParser;
            _s13Parser = s13Parser;
            _tutByParser = tutByParser;
            _userManager = userManager;
        }

        public IActionResult Index()
        {


            var news = _unitOfWork.News.AsQueryable().Include(article => article.Source)
                                                     .OrderByDescending(article => article.DatePublication);
            return View(news);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Parse()
        {
            _onlinerParser.Parse(ONLINER);
            _s13Parser.Parse(S13);
            _tutByParser.Parse(TUTBY);

            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = await _unitOfWork.News.GetByIdAsync(id);
            var comments = _unitOfWork.Comments.AsQueryable().Include(c=>c.User).Where(c => c.ArticleId.Equals(id)).OrderByDescending(c=>c.Date);

            if (article == null)
            {
                return NotFound();
            }

            var articleViewModel = new ArticleViewModel()
            {
                Article = article,
                Comments = comments
            };

            return View(articleViewModel);
        }
    }
}
