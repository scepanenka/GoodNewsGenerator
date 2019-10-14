using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace Services.Parsers
{
    public class TutByParser : NewsParser, ITutByParser
    {
                static HttpClient client;
        static HtmlWeb web = new HtmlWeb();
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _url = @"https://news.tut.by/rss/all.rss";

        public TutByParser(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override IEnumerable<Article> GetFromRss()
        {
            XmlReader feedReader = XmlReader.Create(_url);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);

            List<Article> news = new List<Article>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    string link = article.Links.FirstOrDefault().Uri.ToString();
                    string articleUrl = link.Substring(0, link.LastIndexOf("?"));

                    news.Add(new Article()
                    {
                        Title = article.Title.Text.Replace("&nbsp;", string.Empty),
                        Description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty),
                        DateOfPublication = article.PublishDate.UtcDateTime,
                        Content = GetTextOfArticle(articleUrl),
                        Url = articleUrl,
                        Category = _unitOfWork.GetOrCreateCategory(article.Categories.FirstOrDefault().Name),
                        Source = _unitOfWork.Sources.AsQueryable().FirstOrDefault(x => x.Url.Contains(_url))
                    }
                    );
                }
            }

            return news;
        }


        public override string GetTextOfArticle(string url)
        {
            WebClient wc = new WebClient();
            string htmlText = wc.DownloadString(url);
            wc.Dispose();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);
            HtmlNode article = doc.QuerySelector("#article_body");
            string content = "";
            if (article != null)
            {
                content = article.InnerHtml;
            }

            content = Regex.Replace(content, @"\s+", " ");

            return HttpUtility.HtmlDecode(content);
        }
    }
}
