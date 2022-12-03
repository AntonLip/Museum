using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models.Interfaces.Service
{
    public interface IExhibitService : IService<Exhibit, ExhibitDto, AddExhibitDto, AddExhibitDto, Guid>
    {
        List<ExhibitDto> GetByCategoryId(Guid id);
        List<ExhibitDto> GetByPartName(string name);
        Task<AddExhibitDto> GetByIdForUpdateAsync(Guid id);
    }
}
