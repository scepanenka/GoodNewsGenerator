using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.Data.Entities;


namespace Services.Parsers
{
    public class NewsParser : INewsParser
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsParser(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Add(Article article)
        {
            if (!_unitOfWork.News.Find(a => a.Url == article.Url).Any())
            {
                _unitOfWork.News.Add(article);
                return true;
            }
            _unitOfWork.News.Add(article);
            return true;
        }

        public virtual async Task<bool> AddNewsAsync(IEnumerable<Article> news)
        {
            foreach (var article in news)
            {
                if (!_unitOfWork.News.Find(a => a.Url == article.Url).Any())
                {
                    _unitOfWork.News.Add(article);
                }
            }
            await _unitOfWork.SaveAsync();
            return true;
        }

        public virtual IEnumerable<Article> GetFromRss()
        {
            return null;
        }

        public virtual string GetTextOfArticle(string url)
        {
            return string.Empty;
        }

        public async Task Parse()
        {
            await AddNewsAsync(GetFromRss());
        }
    }
}
