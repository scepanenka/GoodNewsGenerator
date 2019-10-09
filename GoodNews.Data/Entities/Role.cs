using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodNews.Data.Entities
{
    public class Role : Entity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
