using System;
using System.ComponentModel.DataAnnotations;

namespace GoodNews.MVC.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
