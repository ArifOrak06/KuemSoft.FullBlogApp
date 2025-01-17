using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class TagSeedData : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(new Tag[]
            {
                new()
                {
                    Id = Guid.Parse("9679A96B-E6E6-44A5-B04E-20C80D70BD4B"),
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = "Adminastrator",
                     ModifiedBy = "Admiastrator",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     Text = "Web Programlama"
                },
                new()
                {
                    Id = Guid.Parse("8183E35A-277B-4C1F-8A66-D75F68B80BF5"),
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = "Adminastrator",
                     ModifiedBy = "Admiastrator",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     Text = "Mobil Programlama"
                },
                new()
                {
                    Id = Guid.Parse("10B0D58B-8155-48F7-A334-977513EC67D0"),
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = "Adminastrator",
                     ModifiedBy = "Admiastrator",
                     CreatedDate = DateTime.Now,
                     ModifiedDate = DateTime.Now,
                     Text = "Masaüstü Programlama"
                },

            });
        }
    }
}
