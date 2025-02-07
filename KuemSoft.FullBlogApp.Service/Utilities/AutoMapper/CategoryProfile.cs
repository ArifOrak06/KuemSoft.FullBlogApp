using AutoMapper;
using KuemSoft.FullBlogApp.Core.DTOs.CategoryDTOs;
using KuemSoft.FullBlogApp.Core.Entities.Concrete;

namespace KuemSoft.FullBlogApp.Service.Utilities.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryUpdateDto, CategoryCreateDto>().ReverseMap();
        }
    }
}
