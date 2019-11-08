using System;

namespace GoodNews.Data.Entities
{
    public class Comment : Entity
    {
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int LikesCount { get; set; }

    }
}
