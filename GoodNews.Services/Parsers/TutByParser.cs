using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using GoodNews.Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace GoodNews.Services.Parsers
{
    public class TutByParser : NewsParser, ITutByParser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _url = @"https://news.tut.by/rss/all.rss";


        public TutByParser(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override IEnumerable<Article> GetNews()
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
                        string content = GetArticleContent(url);

                        news.Add(new Article()
                            {
                                Title = article.Title.Text.Replace("&nbsp;", string.Empty),
                                Description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty),
                                DatePublication = article.PublishDate.UtcDateTime,
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

                string content = GetArticleContent(articleUrl);

                thumbnailUrl = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            }

            return thumbnailUrl;
        }


        public override string GetArticleContent(string url)
        {
            WebClient wc = new WebClient();
            string htmlText = wc.DownloadString(url);
            wc.Dispose();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);
            HtmlNode article = doc.QuerySelector("#article_body");
            if (article != null)
            {
                var badNodes = article.ChildNodes
                    .Where(a => (a.HasClass("b-addition")))
                    .ToList();
                foreach (var node in badNodes)
                    node.Remove();
                string content = "";
                content = article.InnerHtml;
                content = Regex.Replace(content, @"\s+", " ");

                return HttpUtility.HtmlDecode(content);
            }

            return string.Empty;
        }
    }
}
