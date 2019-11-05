using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace Services.Parsers
{
    public class TutByParser : NewsParser, ITutByParser
    {
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
            Source source = _unitOfWork.Sources.AsQueryable().FirstOrDefault(x => x.Url.Contains(_url));

            List<Article> news = new List<Article>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    string url = article.Links.FirstOrDefault().Uri.ToString();
                    if (_unitOfWork.News.Find(a => a.Url.Equals(url)).FirstOrDefault() == null)
                    {
                        string content = GetTextOfArticle(url);

                        news.Add(new Article()
                            {
                                Title = article.Title.Text.Replace("&nbsp;", string.Empty),
                                Description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty),
                                DateOfPublication = article.PublishDate.UtcDateTime,
                                Content = content,
                                Url = url,
                                Category = _unitOfWork.GetOrCreateCategory(article.Categories.FirstOrDefault().Name),
                                Source = source,
                                ThumbnailUrl = GetThumbnail(article)
                            }
                        );
                    }
                }
            }
            return news;
        }

        public override string GetThumbnail(SyndicationItem article)
        {
            string thumbnailUrl = article.ElementExtensions
                .Where(extension => extension.OuterName == "thumbnail")
                .Select(extension => (string)extension.GetObject<XElement>().Attribute("url"))
                .FirstOrDefault();

            if (thumbnailUrl == null)
            {
                thumbnailUrl = article.ElementExtensions
                    .Where(extension => extension.OuterName == "content" && (((string)extension.GetObject<XElement>().Attribute("type") == "image/jpeg")) 
                                                                            || ((string)extension.GetObject<XElement>().Attribute("type") == "image/gif"))
                    .Select(extension => (string)extension.GetObject<XElement>().Attribute("url"))
                    .FirstOrDefault();
            }

            if (thumbnailUrl == null)
            {
                string link = article.Links.FirstOrDefault().Uri.ToString();
                string articleUrl = link.Substring(0, link.LastIndexOf("?"));

                string content = GetTextOfArticle(articleUrl);

                thumbnailUrl = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            }

            return thumbnailUrl;
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
