using System;
using System.Collections.Generic;

namespace GoodNews.Data.Entities
{
    public class Article : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }        
        public DateTime DateOfPublication { get; set; }        

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid SourceId { get; set; }
        public Source Source { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
