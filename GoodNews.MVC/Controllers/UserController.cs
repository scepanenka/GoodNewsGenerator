using System.Linq;
using System.Threading.Tasks;
using GoodNews.BL.ViewModels;
using GoodNews.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoodNews.MVC.Controllers
{

    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET
        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View(userViewModel);

            var user = new User()
            {
                Email = userViewModel.Email,
                UserName = userViewModel.Email,
                BirthDate = userViewModel.BirthDate
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userViewModel);
        }


        public async Task<IActionResult> Edit(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var vm = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                BirthDate = user.BirthDate
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View(userViewModel);

            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.BirthDate = userViewModel.BirthDate;
            user.Email = userViewModel.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userViewModel);
        }
    }
}
