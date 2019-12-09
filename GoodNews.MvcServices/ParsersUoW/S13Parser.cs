using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using GoodNews.Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace GoodNews.MvcServices.ParsersUoW
{
    public class S13Parser : ParserUoW, IS13Parser
    {
        private readonly IUnitOfWork _unitOfWork;

        public S13Parser(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Article>> GetNewsAsync(string s13Url)
        {
            XmlReader feedReader = XmlReader.Create(s13Url);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);
            Source source = _unitOfWork.Sources.AsQueryable().FirstOrDefault(x => x.Url.Contains(s13Url));

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
                    .Where(extension => extension.OuterName == "content" && (string)extension.GetObject<XElement>().Attribute("type") == "image/jpeg")
                    .Select(extension => (string)extension.GetObject<XElement>().Attribute("url"))
                    .FirstOrDefault();
            }

            if (thumbnailUrl == null)
            {
                string link = article.Links.FirstOrDefault().Uri.ToString();

                WebClient wc = new WebClient();
                string htmlText = wc.DownloadString(link);
                wc.Dispose();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlText);

                string content = doc.QuerySelector(".content").InnerHtml;
                
                thumbnailUrl = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                if (thumbnailUrl.StartsWith("/ru"))
                {
                    thumbnailUrl = thumbnailUrl.Insert(0, "http://s13.ru");
                }
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

            HtmlNode article = doc.QuerySelector(".js-mediator-article");
            string content = "";
            if (article != null)
            {
                content = article.InnerHtml;
            }

            content = Regex.Replace(content, @"\s+", " ");
            content = Regex.Replace(content, @"src=""/ru/", @"src=""http://s13.ru/ru/");

            return HttpUtility.HtmlDecode(content);
        }
    }
}
