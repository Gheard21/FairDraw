using FairDraw.App.Core.Entities;
using FairDraw.App.Infrastructure.Interfaces;

namespace FairDraw.App.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>(DataContext dataContext) : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public async Task AddAsync(TEntity entity)
            => await dataContext.Set<TEntity>().AddAsync(entity);

        public async Task<TEntity?> FindAsync(Guid id) =>
            await dataContext.Set<TEntity>().FindAsync(id);

        public async Task SaveChangesAsync()
            => await dataContext.SaveChangesAsync();
    }
}
