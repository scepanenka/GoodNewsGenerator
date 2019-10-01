using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.DAL.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }


    }
}
