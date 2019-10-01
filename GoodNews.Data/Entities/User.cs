using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.DAL.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }

    }
}
