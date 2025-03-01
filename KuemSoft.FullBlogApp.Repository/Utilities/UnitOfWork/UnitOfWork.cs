using KuemSoft.FullBlogApp.Core.UnitOfWork;
using KuemSoft.FullBlogApp.Repository.Contexts.EfCore;

namespace KuemSoft.FullBlogApp.Repository.Utilities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
