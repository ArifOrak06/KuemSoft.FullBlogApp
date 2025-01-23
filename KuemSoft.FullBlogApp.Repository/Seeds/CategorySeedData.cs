using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
            {
                new()
                {
                    Id = Guid.Parse("B6A67185-45C5-4707-AF76-1B55ED3C3B6A"),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Adminastrator",
                    Description = "Mobile Application Development",
                    IsActive = true,
                    IsDeleted = false,
                    ModifiedBy = "Adminastrator",
                    ModifiedDate = DateTime.Now,
                },
                     new()
                {
                    Id = Guid.Parse("6647B6EF-B52B-4EF8-ABB1-F32360323BD6"),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Adminastrator",
                    Description = "Web Application Development",
                    IsActive = true,
                    IsDeleted = false,
                    ModifiedBy = "Adminastrator",
                    ModifiedDate = DateTime.Now,
                }
            });
        }
    }
}
