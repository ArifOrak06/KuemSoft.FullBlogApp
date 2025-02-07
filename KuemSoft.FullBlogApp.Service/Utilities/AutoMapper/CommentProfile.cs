using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.CommentDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentCreateDto>().ReverseMap();
            CreateMap<Comment, CommentUpdateDto>().ReverseMap();

            CreateMap<CommentDto, CommentCreateDto>().ReverseMap();
            CreateMap<CommentDto, CommentUpdateDto>().ReverseMap();
            CreateMap<CommentCreateDto, CommentUpdateDto>().ReverseMap();
        }
    }
}
