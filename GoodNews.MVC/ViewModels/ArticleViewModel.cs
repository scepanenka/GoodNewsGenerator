using System.Collections.Generic;
using GoodNews.Data.Entities;

namespace GoodNews.MVC.ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
