using System.ComponentModel.DataAnnotations;


namespace GoodNews.API.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 3)]
        public string Password { get; set; }
    }
}
