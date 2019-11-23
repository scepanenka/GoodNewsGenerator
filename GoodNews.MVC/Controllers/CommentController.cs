using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CommentController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment newComment)
        {
            var user = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var comment = new Comment()
            {
                Id = new Guid(),
                User = user,
                Content = newComment.Content,
                Date = newComment.Date.ToLocalTime(),
                Article = _unitOfWork.News.Find(a => a.Id.Equals(newComment.ArticleId)).FirstOrDefault()
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();

            var result = new CommentViewModel
            {
                Id = comment.Id,
                Author = comment.User.UserName,
                Date = comment.Date.ToString(),
                Content = comment.Content
            };
            
            return Json(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> _GetComments(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = await _unitOfWork.News.GetByIdAsync(id);
            var comments = _unitOfWork.Comments.AsQueryable().Include(c => c.User)
                .Where(c => c.ArticleId.Equals(id)).OrderByDescending(c => c.Date);

            if (article == null)
            {
                return NotFound();
            }

            var articleViewModel = new ArticleViewModel()
            {
                Article = article,
                Comments = comments
            };

            return PartialView("_GetComments", articleViewModel);
        }
    }

    
}