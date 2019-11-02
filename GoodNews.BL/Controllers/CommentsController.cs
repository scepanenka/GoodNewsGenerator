using System.Threading.Tasks;
using Core;
using GoodNews.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.BL.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Comments()
        {
            var comments = _unitOfWork.Comments.AsQueryable().Include(c => c.User.UserName);
            return PartialView("_Comments",comments);
        }



    }
}