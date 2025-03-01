using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class CommentSeedData : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(new Comment[]
            {
                new()
                {
                    Id = Guid.Parse("6D34DB5E-B9D5-458F-BE25-0DE3E05B9CAC"),
                    AppUserId = Guid.Parse("AA8504D6-2B32-4E89-8EC0-2F4EBE57074B"),
                    ArticleId = Guid.Parse("40B63CD3-D9D4-4E47-906F-EA4564B4D827"),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy  = "Adminastrator",
                    ModifiedBy = "Adminastrator",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Text = "Harika bir makale, ama bu yorum bir test yorumudur.",
                },
                new()
                {
                    Id = Guid.Parse("F2292884-2B04-43C4-A11D-45DBF39B244E"),
                    AppUserId = Guid.Parse("F19CC326-05F2-4305-AD39-F4E0645AECA0"),
                    ArticleId = Guid.Parse("C1B57612-0F59-4D8D-956E-07E40FC7734A"),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy  = "Adminastrator",
                    ModifiedBy = "Adminastrator",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Text = "Kötü bir makale, anlamak mümkün değildir., ama bu yorum ikinci test yorumudur.",
                }
            });
        }
    }
}
