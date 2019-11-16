using System;
using System.ComponentModel.DataAnnotations;

namespace GoodNews.MVC.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
