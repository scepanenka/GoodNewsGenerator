using System;

namespace GoodNews.API.Models
{
    public class ArticleNewsPageViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime DatePublication { get; set; }
        public double SentimentRating { get; set; }
        public string Source { get; set; }

        public string Category { get; set; }

    }
}
