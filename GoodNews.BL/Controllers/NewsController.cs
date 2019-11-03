using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core;
using GoodNews.BL.ViewModels;
using GoodNews.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

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
                                                     .OrderByDescending(article => article.DateOfPublication);
            return View(news);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(string content, Guid articleId)
        {
            var user = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            
            var comment = new Comment()
            {
                Id = new Guid(),
                User = user,
                Content = content,
                Date = DateTime.Now.ToUniversalTime(),
                Article = _unitOfWork.News.Find(a => a.Id.Equals(articleId)).FirstOrDefault()
            };

            await _unitOfWork.Comments.AddAsync(comment);

            await _unitOfWork.SaveAsync();


            return RedirectToAction("Details", "News", new { id = articleId });
        }


    }
}
