using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddNews;
using GoodNews.MediatR.Commands.CreateCategory;
using GoodNews.MediatR.Queries.ArticleExists;
using GoodNews.MediatR.Queries.GetSourceByUrl;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MediatR;

namespace GoodNews.ApiServices.Parsers
{
    public class NewsParser : INewsParser
    {
        private readonly IMediator _mediator;

        public NewsParser(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async void Parse(string url)
        {
            AddNews(await GetNewsAsync(url));
        }

        public async void AddNews(IEnumerable<Article> news)
        {
            if (news != null)
            {
                await _mediator.Send(new AddNews(news));
            }
        }

        public async Task<IEnumerable<Article>> GetNewsAsync(string url)
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
                    bool articleExists = await _mediator.Send(new ArticleExists(articleUrl));
                    if (articleExists)
                    {
                        string content = GetArticleContent(articleUrl, source);
                        Category category = await _mediator.Send(new CreateCategory(article.Categories.FirstOrDefault().Name));
                        string title = article.Title.Text.Replace("&nbsp;", string.Empty);
                        string description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty);

                        news.Add(new Article()
                            {
                                Title = title,
                                Description = description,
                                DatePublication = article.PublishDate.UtcDateTime,
                                Content = content,
                                Url = articleUrl,
                                Category = category,
                                Source = source,
                                ThumbnailUrl = GetThumbnail(article)
                            }
                        );
                    }
                }
            }
            return news;
        }

        private string GetArticleContent(string url, Source source)
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
                    .Where(a => (a.Attributes.Contains("style") && a.Attributes["style"].Value.Contains("text-align: right")) ||
                                (a.HasClass("news-media_3by2")) ||
                                (a.HasClass("news-widget")) ||
                                (a.HasClass("b-addition")))
                    .ToList();

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

            return null;
        }

        private string GetArticleText(string url)
        {
            throw new NotImplementedException();
        }

        private string GetThumbnail(SyndicationItem article)
        {
            throw new NotImplementedException();
        }
    }
}
