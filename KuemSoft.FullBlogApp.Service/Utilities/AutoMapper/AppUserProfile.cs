using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser,AppUserDto>().ReverseMap();
            CreateMap<AppUser,AppUserCreateDto>().ReverseMap();
            CreateMap<AppUser, AppUserUpdateDto>().ReverseMap();

            CreateMap<AppUserDto,AppUserCreateDto>().ReverseMap();
            CreateMap<AppUserDto,AppUserUpdateDto>().ReverseMap();
            CreateMap<AppUserCreateDto, AppUserUpdateDto>().ReverseMap();

        }
    }
}
