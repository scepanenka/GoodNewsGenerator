using System;
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
using GoodNews.MediatR.Queries.ArticleExists;
using GoodNews.MediatR.Queries.GetSourceByUrl;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MediatR;
using Serilog;

namespace ParserService
{
    public class NewsParser : IParser
    {
        private readonly IMediator _mediator;

        public NewsParser(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Article>> Parse(string url)
        {
            XmlReader feedReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);
            Source source = await _mediator.Send(new GetSourceByUrl(url));

            List<Article> news = new List<Article>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    string articleUrl = article.Links.FirstOrDefault().Uri.ToString();
                    articleUrl = articleUrl.IndexOf("?") < 0 ? articleUrl : articleUrl.Remove(articleUrl.IndexOf("?"));
                    bool articleExists = await _mediator.Send(new ArticleExists(articleUrl));
                    if (!articleExists && !news.Any(a=>a.Url.Equals(articleUrl)))
                    {
                        string content = GetArticleContent(articleUrl, source);
                        if (!string.IsNullOrEmpty(content))
                        {
                            Category category = new Category()
                                {Name = article.Categories.FirstOrDefault().Name.ToUpper()};
                            string title = article.Title.Text.Replace("&nbsp;", string.Empty);
                            string description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                    .Replace(@"\s+", " ")
                                    .Replace("Читать далее…", string.Empty)
                                ;
                            string articleText = Regex.Replace(content, @"<.*?>|\r\n", string.Empty)
                                .Replace(@"\s+", " ");
                            string thumbnail = GetThumbnail(article, source);
                            if (!news.Any(a => a.Url.Equals(articleUrl)))
                            {
                                news.Add(new Article()
                                {
                                    Title = title,
                                    Description = description,
                                    DatePublication = article.PublishDate.UtcDateTime,
                                    Content = content,
                                    Url = articleUrl,
                                    Category = category,
                                    Source = source,
                                    ThumbnailUrl = thumbnail,
                                    Text = articleText
                                });
                            }
                        }
                        
                    }
                }
            }
            
            return news;
        }



        private string GetArticleContent(string url, Source source)
        {

            try
            {
                string selector = source.QuerySelector;
                WebClient wc = new WebClient();
                string htmlText = wc.DownloadString(url);
                wc.Dispose();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlText);

                HtmlNode article = doc.QuerySelector(selector);
                string content = "";
                if (article != null)
                {
                    var badNodes = article.ChildNodes
                        .Where(a => (a.Attributes.Contains("style") &&
                                     a.Attributes["style"].Value.Contains("text-align: right")) ||
                                    (a.Name == "script") ||
                                    (a.Name == "table") ||
                                    (a.HasClass("news-media_3by2")) ||
                                    (a.HasClass("news-widget")) ||
                                    (a.HasClass("news-banner")) ||
                                    (a.HasClass("news-incut")) ||
                                    (a.HasClass("news-vote")) ||
                                    (a.HasClass("news-reference")) ||
                                    (a.HasClass("TitledImage")) ||
                                    (a.HasClass("news-header")) ||
                                    (a.HasClass("b-addition"))).ToList();

                    foreach (var node in badNodes)
                        node.Remove();

                    content = article.InnerHtml;
                    content = Regex.Replace(content, @"\s+", " ").Replace("Читать далее…", ""); ;
                    if (source.Name == "S13")
                    {
                        content = Regex.Replace(content, @"src=""/ru/", @"src=""http://s13.ru/ru/");
                    }
                    return HttpUtility.HtmlDecode(content);

                }

                return content;
            }
            catch (Exception e)
            {
                Log.Error("Enable parse content");
                return null;
            }
        }

        private string GetThumbnail(SyndicationItem article, Source source)
        {
            
            try
            {
                string selector = source.Name == "S13.RU" ? ".content" : source.QuerySelector;

                string thumbnailUrl = article.ElementExtensions
                    .Where(extension =>
                        extension.OuterName == "content" &&
                        (string)extension.GetObject<XElement>().Attribute("type") == "image/jpeg")
                    .Select(extension => (string)extension.GetObject<XElement>().Attribute("url"))
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(thumbnailUrl))
                {
                    string link = article.Links.FirstOrDefault().Uri.ToString();

                    WebClient wc = new WebClient();
                    string htmlText = wc.DownloadString(link);
                    wc.Dispose();

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlText);

                    string content = doc.QuerySelector(selector).InnerHtml;

                    thumbnailUrl = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase)
                        .Groups[1].Value;
                }
                if (string.IsNullOrEmpty(thumbnailUrl))
                {
                        thumbnailUrl = article.ElementExtensions
                        .Where(extension => extension.OuterName == "thumbnail")
                        .Select(extension => (string)extension.GetObject<XElement>().Attribute("url"))
                        .FirstOrDefault();
                }

                thumbnailUrl = thumbnailUrl.StartsWith("/ru") && source.Name.Equals("S13.RU")
                    ? thumbnailUrl.Insert(0, "http://s13.ru")
                    : thumbnailUrl;

                return thumbnailUrl;
            }
            catch
            {
                return null;
            }
        }
    }
}
