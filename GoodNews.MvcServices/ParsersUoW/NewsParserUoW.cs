using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;

namespace GoodNews.MvcServices.ParsersUoW
{
    public abstract class NewsParserUoW : INewsParser
    {
        private readonly IUnitOfWork _unitOfWork;

        protected NewsParserUoW(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddArticle(Article article)
        {
            if (!_unitOfWork.News.Find(a => a.Url == article.Url).Any())
            {
                _unitOfWork.News.Add(article);
                return true;
            }
            _unitOfWork.News.Add(article);
            return true;
        }

        public virtual async void AddNews(IEnumerable<Article> news)
        {
            foreach (var article in news)
            {
                if (!_unitOfWork.News.Find(a => a.Url == article.Url).Any())
                {
                    await _unitOfWork.News.AddAsync(article);
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<Article>> GetNewsAsync(string url)
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

        public async void Parse(string url)
        {
            AddNews(await GetNewsAsync(url));
        }

        public string GetArticleText(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
