using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models.Interfaces.Service
{
    public interface IExhibitService : IService<Exhibit, ExhibitDto, AddExhibitDto, AddExhibitDto, Guid>
    {

    }
}
