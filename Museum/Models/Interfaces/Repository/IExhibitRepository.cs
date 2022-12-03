using Museum.Models.DbModels;

namespace Museum.Models.Interfaces.Repository
{
    public interface IExhibitRepository : IRepository<Exhibit, Guid>
    {
        List<Exhibit> GetByPartName(string name);
    }
}
