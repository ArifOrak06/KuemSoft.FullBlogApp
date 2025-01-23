using KuemSoft.FullBlogApp.Core.DTOs.AppRoleDTOs;
using Microsoft.AspNetCore.Http;

namespace KuemSoft.FullBlogApp.Core.DTOs.AppUserDTOs
{
    public class AppUserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
        public Guid AppRoleId { get; set; }
        public ICollection<AppRoleDto> AppRoles { get; set; }

    }
}
