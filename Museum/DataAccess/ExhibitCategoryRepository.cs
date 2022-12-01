using Museum.Models.DbModels;
using Museum.Models.Interfaces.Repository;

namespace Museum.DataAccess
{
    public class ExhibitCategoryRepository : BaseRepository<ExhibitCategory>, IExhibitCategoryRepository
    {
        public ExhibitCategoryRepository(AppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
