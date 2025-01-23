using KuemSoft.FullBlogApp.Core.Repositories;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;
using KuemSoft.FullBlogApp.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KuemSoft.FullBlogApp.Repository.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddDependenciesToRepositoryLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), opt =>
                {
                    opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
                
            });

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        }

    }
}
