using AutoMapper;
using GoodNews.API.Models;
using GoodNews.Data.Entities;


namespace GoodNews.API.Mappings
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleNewsPageViewModel>()
                .ForMember(dest => dest.Category, category => category.MapFrom(c => c.Category.Name))
                .ForMember(dest => dest.Source, source => source.MapFrom(src => src.Source.Name));
        }
    }
}
