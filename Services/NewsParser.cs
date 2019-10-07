using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Core;
using GoodNews.Data.Entities;


namespace Services
{
    public class NewsParser : INewsParser
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string url = @"https://realt.onliner.by/feed";
        public bool Add(Article article)
        {
            _unitOfWork.News.Insert(article);
            return true;
        }

        public bool AddRange(IEnumerable<Article> articles)
        {
            _unitOfWork.News.AddRange(articles);
            return true;
        }

        public IEnumerable<Article> GetFromUrl(string url)
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            StringBuilder rssContent = new StringBuilder();

            XmlReader feedReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);


            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";
                
            }

            List<Article> news = new List<Article>();

            return news;
        }
    }
}
