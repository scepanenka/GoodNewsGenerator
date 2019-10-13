using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Core;
using GoodNews.Data.Entities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace Services.Parsers
{
    public class S13Parser : NewsParser, IS13Parser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _url = @"http://s13.ru/rss";

        public S13Parser(IUnitOfWork unitOfWork) : base(unitOfWork)
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
                    news.Add(new Article()
                    {
                        Title = article.Title.Text.Replace("&nbsp;", string.Empty),
                        Description = Regex.Replace(article.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty),
                        DateOfPublication = article.PublishDate.UtcDateTime,
                        Content = GetTextOfArticle(article.Links.FirstOrDefault().Uri.ToString()),
                        Url = article.Links.FirstOrDefault().Uri.ToString(),
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
            string text = "";

            IList<HtmlNode> nodes = doc.QuerySelectorAll(".js-mediator-article p");

            foreach (var item in nodes)
            {
                if (text == "")
                {
                    text = item.InnerText;
                }
                else
                {
                    text += Environment.NewLine + item.InnerText;
                }
            }

            text = Regex.Replace(text, @"\s+", " ").Replace("&nbsp;", string.Empty);

            return HttpUtility.HtmlDecode(text);
        }
    }
}
