using KuemSoft.FullBlogApp.Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KuemSoft.FullBlogApp.Repository.Contexts.EfCore
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        DbSet<Article> Articles { get; set; }
        DbSet<Comment> Comments {  get; set; }  
        DbSet<Tag> Tags { get; set; }
    }
}
