using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GoodNews.Data.Entities
{
    public class Article : Entity
    {
        public int SourceId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Source Source { get; set; }
    }
}
