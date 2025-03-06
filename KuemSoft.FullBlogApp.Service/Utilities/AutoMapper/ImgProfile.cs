using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class ImgProfile : Profile
    {
        public ImgProfile()
        {
            CreateMap<Img, ImgDto>().ReverseMap();
        }
    }
}
