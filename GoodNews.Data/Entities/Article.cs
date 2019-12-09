using System;
using System.Collections.Generic;

namespace GoodNews.Data.Entities
{
    public class Article : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }        
        public string ThumbnailUrl { get; set; }        
        public DateTime DatePublication { get; set; }
        public double IndexPositivity { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid SourceId { get; set; }
        public Source Source { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
