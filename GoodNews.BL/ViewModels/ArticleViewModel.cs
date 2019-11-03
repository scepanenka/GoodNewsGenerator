using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.BL.ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
