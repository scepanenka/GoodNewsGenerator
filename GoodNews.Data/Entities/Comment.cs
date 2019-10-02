using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.DAL.Entities
{
    public class Comment : Entity
    {
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }


    }
}
