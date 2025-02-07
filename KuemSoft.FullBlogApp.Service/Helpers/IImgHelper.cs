using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace KuemSoft.FullBlogApp.Service.Helpers
{
    public interface IImgHelper
    {
        Task<CustomResponseDto<ImgUploadDto>> UploadImageAsync(string name, IFormFile formFile, ImageType imageType, string folderName = null);
        Task<CustomResponseDto<NoContentDto>> RemoveImageAsync(string imageName);




    }
}
