using System;
using System.Collections.Generic;
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

namespace GoodNews.BL.Controllers
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
                Date = DateTime.Now,
                Article = _unitOfWork.News.Find(a => a.Id.Equals(newComment.ArticleId)).FirstOrDefault()
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            
            return Json(comment);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> _GetComments(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = _unitOfWork.News.GetById(id);
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