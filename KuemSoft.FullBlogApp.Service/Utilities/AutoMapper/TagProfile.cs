using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.TagDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, TagCreateDto>().ReverseMap();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
            CreateMap<TagDto, TagUpdateDto>().ReverseMap();
            CreateMap<TagDto, TagCreateDto>().ReverseMap();
            CreateMap<TagUpdateDto, TagCreateDto>().ReverseMap();
        }
    }
}
