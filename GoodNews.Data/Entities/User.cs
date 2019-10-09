using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodNews.Data.Entities
{
    public class User : Entity
    {

        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
