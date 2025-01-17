using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuemSoft.FullBlogApp.Repository.Seeds
{
    public class ArticleSeedData : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article[]
            {
                new()
                {
                    Id = Guid.Parse("40B63CD3-D9D4-4E47-906F-EA4564B4D827"),
                    AppUserId = Guid.Parse("AA8504D6-2B32-4E89-8EC0-2F4EBE57074B"),
                    Title ="ASP.Net Core Teknolojisi İle Web backend Geliştirmek",
                    Content = "ASP.Net Core Teknolojisi İle Web backend GeliştirmekASP.Net Core Teknolojisi İle Web backend GeliştirmekASP.Net Core Teknolojisi İle Web backend GeliştirmekASP.Net Core Teknolojisi İle Web backend GeliştirmekASP.Net Core Teknolojisi İle Web backend GeliştirmekASP.Net Core Teknolojisi İle Web backend Geliştirmek",
                    CreatedBy = "Adminastrator",
                    ModifiedBy = "Adminastrator",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,

                },
                  new()
                {
                    Id = Guid.Parse("C1B57612-0F59-4D8D-956E-07E40FC7734A"),
                    AppUserId = Guid.Parse("AA8504D6-2B32-4E89-8EC0-2F4EBE57074B"),
                    Title ="NodeJs ile Restfull Web API Geliştirmek",
                    Content = "ANodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API GeliştirmekNodeJs ile Restfull Web API Geliştirmek",
                    CreatedBy = "Adminastrator",
                    ModifiedBy = "Adminastrator",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,

                }
            });

        }
    }
}
