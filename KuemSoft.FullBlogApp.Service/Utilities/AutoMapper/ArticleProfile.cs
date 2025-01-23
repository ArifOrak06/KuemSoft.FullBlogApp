using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.ArticleDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Article, ArticleCreateDto>().ReverseMap();
            CreateMap<Article, ArticleUpdateDto>().ReverseMap();

            CreateMap<ArticleDto,ArticleCreateDto>().ReverseMap();
            CreateMap<ArticleDto,ArticleUpdateDto>().ReverseMap();
            CreateMap<ArticleCreateDto, ArticleUpdateDto>().ReverseMap();
        }
    }
}
