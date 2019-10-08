using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;


namespace Services
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
            _unitOfWork.News.Insert(article);
            return true;
        }

        public bool AddRange(IEnumerable<Article> news)
        {
            _unitOfWork.News.AddRange(news);
            return true;
        }

        public IEnumerable<Article> GetFromUrl(string url)
        {
            XmlReader feedReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);
            
            List<Article> news = new List<Article>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    news.Add(new Article()
                        {
                            Title = article.Title.Text.Replace("&nbsp;", ""),
                            Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                            DateOfPublication = article.PublishDate.UtcDateTime,
                            Content = GetTextOfArticle(article.Links.FirstOrDefault().Uri.ToString())
                    }
                    );
                }
            }

            return news;
        }

        public string GetTextOfArticle(string url)
        {
                string text = "Test Test Test";

                return text;
            }
    }
}
