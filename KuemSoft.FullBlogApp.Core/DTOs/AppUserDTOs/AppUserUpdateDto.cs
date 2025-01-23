using KuemSoft.FullBlogApp.Core.DTOs.AppRoleDTOs;
using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using Microsoft.AspNetCore.Http;

namespace KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs
{
    public class AppUserUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ImgId{ get; set; }
        public ImgDto Img { get; set; }
        public IFormFile Photo { get; set; }
        public Guid? AppRoleId { get; set; }
        public string? Role { get; set; }
        public IList<AppRoleDto> AppRoles { get; set; }
 
    }
}
