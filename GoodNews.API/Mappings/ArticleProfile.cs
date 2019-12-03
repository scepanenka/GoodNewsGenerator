using AutoMapper;
using GoodNews.API.Models;
using GoodNews.Data.Entities;


namespace GoodNews.API.Mappings
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDTO>();
        }
        
    }
}
