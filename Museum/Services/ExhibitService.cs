using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces.Service;
using Museum.Models.Interfaces.Repository;

namespace Museum.Services
{
    public class ExhibitService : BaseService<Exhibit, ExhibitDto, AddExhibitDto, AddExhibitDto>, IExhibitService
    {
        
        public ExhibitService(IExhibitRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
           
        }
        
    }
}
