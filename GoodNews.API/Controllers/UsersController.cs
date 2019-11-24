using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.API.Models;
using GoodNews.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _userManager.Users.ToListAsync());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string userId)
        {
            try
            {
                return Ok(_userManager.Users.Where(s => s.Id.Equals(userId)).ToString());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User { Email = model.Email, UserName = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    await _userManager.AddToRoleAsync(user, "user");
                }
                catch
                {
                    return StatusCode(500);
                }

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var role = await _userManager.FindByIdAsync(userId);
                    await _userManager.DeleteAsync(role);
                }
                catch
                {
                    return StatusCode(500);
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
