using System.Linq.Expressions;
using Museum.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Museum.Models.Interfaces.Repository;

namespace Museum.DataAccess
{
    public class BaseRepository<TModel> : IRepository<TModel, Guid>
        where TModel : class, IEntity<Guid>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TModel> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }
        public virtual async Task AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
        }
        public TModel GetFirst(Func<TModel, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).FirstOrDefault();
        }
        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().OrderBy(l => l.Id).ToListAsync();
        }
        public async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstAsync(l => l.Id == id);
        }
        public async Task RemoveAsync(TModel model, CancellationToken cancellationToken = default)
        {
            _context.Entry(model).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public List<TModel> GetWithInclude(params Expression<Func<TModel, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
        public int GetCount()
        {
            return _dbSet.Count();
        }
        public List<TModel> GetWithInclude(Func<TModel, bool> predicate,
            params Expression<Func<TModel, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }
        private IQueryable<TModel> Include(params Expression<Func<TModel, object>>[] includeProperties)
        {
            IQueryable<TModel> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
