using System;

namespace GoodNews.Data.Entities
{
    public class UserRole : Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
