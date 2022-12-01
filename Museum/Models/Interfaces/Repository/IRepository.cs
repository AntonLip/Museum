using System.Linq.Expressions;

namespace Museum.Models.Interfaces.Repository
{
    public interface IRepository<TModel, TId>
        where TModel : IEntity<TId>
    {
        Task AddAsync(TModel obj, CancellationToken cancellationToken = default);
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TModel> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task UpdateAsync(TModel obj, CancellationToken cancellationToken = default);

        public int GetCount();
        TModel GetFirst(Func<TModel, bool> predicate);
        List<TModel> GetWithInclude(params Expression<Func<TModel, object>>[] includeProperties);
        Task RemoveAsync(TModel model, CancellationToken cancellationToken = default);
        List<TModel> GetWithInclude(Func<TModel, bool> predicate,
            params Expression<Func<TModel, object>>[] includeProperties);
    }
}
