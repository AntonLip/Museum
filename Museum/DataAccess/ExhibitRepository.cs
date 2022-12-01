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
    }
}
