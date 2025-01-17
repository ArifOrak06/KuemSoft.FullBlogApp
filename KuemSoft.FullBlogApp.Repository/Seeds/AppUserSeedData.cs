using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class AppUserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var superAdmin = new AppUser
            {
                Id = Guid.Parse("AA8504D6-2B32-4E89-8EC0-2F4EBE57074B"),
                Email = "superadmin@kuemsoft.com",
                NormalizedEmail = "SUPERADMIN@KUEMSOFT.COM",
                UserName = "superadmin@kuemsoft.com",
                NormalizedUserName = "SUPERADMIN@KUEMSOFT.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                FirstName ="Arif",
                LastName = "ORAK",
                PhoneNumber = "0111 222 33 44",
                PhoneNumberConfirmed = true,
                

            };

            var member = new AppUser
            {
                Id = Guid.Parse("F19CC326-05F2-4305-AD39-F4E0645AECA0"),
                Email = "member@kuemsoft.com",
                NormalizedEmail = "MEMBER@KUEMSOFT.COM",
                UserName = "member@kuemsoft.com",
                NormalizedUserName = "MEMBER@KUEMSOFT.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                FirstName = "Alparslan",
                LastName = "ORAK",
                PhoneNumber = "0111 222 33 44",
                PhoneNumberConfirmed = true,
            };

            superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123456");
            member.PasswordHash = CreatePasswordHash(member, "123456");

            builder.HasData(superAdmin, member);
        }

        private string CreatePasswordHash(AppUser user,string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
