using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoodNews.DAL.Entities
{
    public class User : Entity
    {

        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
