using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Data.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Article> News { get; set; }

    }
}
