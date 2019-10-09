using System.Collections.Generic;

namespace GoodNews.Data.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> News { get; set; }

    }
}
