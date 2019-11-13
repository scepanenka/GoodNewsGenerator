using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.BL.ViewModels
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
