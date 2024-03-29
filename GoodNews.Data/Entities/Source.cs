﻿using System.Collections.Generic;

namespace GoodNews.Data.Entities
{
    public class Source : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string QuerySelector { get; set; }
        public ICollection<Article> News { get; set; }
    }
}
