using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using GoodNews.Core;
using GoodNews.Data.Entities;

namespace GoodNews.MvcServices.ParsersUoW
{
    public abstract class NewsParser : INewsParser
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

        public virtual bool AddNews(IEnumerable<Article> news)
        {
            foreach (var article in news)
            {
                if (!_unitOfWork.News.Find(a => a.Url == article.Url).Any())
                {
                    _unitOfWork.News.Add(article);
                }
            }
            _unitOfWork.Save();
            return true;
        }

        public virtual IEnumerable<Article> GetNews(string url)
        {
            return null;
        }

        public virtual string GetArticleContent(string url)
        {
            return string.Empty;
        }
        public virtual string GetThumbnail(SyndicationItem article)
        {
            return string.Empty;
        }

        public void Parse(string url)
        {
            AddNews(GetNews(url));
        }

        public string GetArticleText(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
