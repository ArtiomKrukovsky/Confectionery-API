namespace Confectionery.Domain.Seedwork
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<List<TEntity>> GetAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
