using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddNewsAndCategories;
using GoodNews.MediatR.Queries.GetSources;
using MediatR;

namespace NewsService
{
    public class NewsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IParser _parser;
        private readonly IRatingService _rating;

        public NewsService(IMediator mediator, IParser parser, IRatingService rating)
        {
            _mediator = mediator;
            _parser = parser;
            _rating = rating;
        }

        public async Task<bool> Run()
        {
            var news = await ParseNews();
            await AddNewsToDb(news);

            var unratedNews = await _rating.GetUnratedFromDb();
            var ratedNews = await _rating.ScoreNewsRatings(unratedNews);
            await _rating.SaveRatingsToDB(ratedNews);

            return true;
        }

        private async Task<IEnumerable<Article>> ParseNews()
        {
            IEnumerable<Source> sources = await GetSourcesFromDb();
            List<Article> news = new List<Article>();
            foreach (Source source in sources)
            {
                news.AddRange(await _parser.Parse(source.Url));
            }

            return news;
        }

        private async Task<IEnumerable<Source>> GetSourcesFromDb()
        {
            return await _mediator.Send(new GetSources());
        }


        private async Task<bool> AddNewsToDb(IEnumerable<Article> news)
        {
            try
            {
                await _mediator.Send(new AddNewsAndCategories(news));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}