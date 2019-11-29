using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddNews;
using GoodNews.MediatR.Commands.CreateCategory;
using GoodNews.MediatR.Queries.ArticleExists;
using GoodNews.MediatR.Queries.GetSourceByUrl;
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
                        string content = GetArticleContent(articleUrl);
                        Category category = await _mediator.Send(new CreateCategory(article.Categories.FirstOrDefault().Name));

                        news.Add(new Article()
                            {
                                Title = article.Title.Text.Replace("&nbsp;", string.Empty),
                                Description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty),
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

        public string GetArticleContent(string url)
        {
            throw new NotImplementedException();
        }

        public string GetArticleText(string url)
        {
            throw new NotImplementedException();
        }

        public string GetThumbnail(SyndicationItem article)
        {
            throw new NotImplementedException();
        }
    }
}
