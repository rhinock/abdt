using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _02.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<long> Create<T>(T entity) where T : IEntity
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update<T>(T entity) where T : IEntity
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : IEntity
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public IQueryable<T> Query<T>() where T : class, IEntity 
            => context.Set<T>().AsNoTracking();
    }
}