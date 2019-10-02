using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.DAL.Entities
{
    public class Source : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> News { get; set; }
    }
}
