using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class AppRoleSeedData : IEntityTypeConfiguration<AppRole>
    {
        
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new AppRole[]
            {
                new()
                {
                    Id = Guid.Parse("7143E119-7E8F-4205-ABB2-41434D76F197"),
                     Name = "superadmin",
                     NormalizedName = "SUPERADMIN"
                },
                new()
                {
                    Id = Guid.Parse("76A068B9-09D7-42F5-B374-D54B18C51506"),
                    Name = "admin",
                    NormalizedName = "ADMIN",
                },
                   new()
                {
                    Id = Guid.Parse("C4D92192-8316-45CD-84DC-E81DC5B80B8B"),
                    Name = "member",
                    NormalizedName = "MEMBER",
                }
                  
            });
        }
    }
}
