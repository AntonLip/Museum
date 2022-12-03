using Microsoft.EntityFrameworkCore;
using Museum.Models.DbModels;
using Museum.Models.Interfaces.Repository;

namespace Museum.DataAccess
{
    public class ExhibitRepository : BaseRepository<Exhibit>, IExhibitRepository
    {
        public ExhibitRepository(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public List<Exhibit> GetByPartName(string name)
        {
            var items = _dbSet.AsNoTracking().Where(p => EF.Functions.Like(p.Name, string.Format("%{0}%", name))).ToList();            
            return items;
        }
    }
}
