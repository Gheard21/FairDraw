using FairDraw.App.Core.Entities;

namespace FairDraw.App.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity?> FindAsync(Guid id);
        Task SaveChangesAsync();
    }
}
