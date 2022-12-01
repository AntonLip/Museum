using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.Interfaces.Repository;
using Museum.Models.Interfaces.Service;

namespace Museum.Services
{
    public class ExhibitCategoryService : BaseService<ExhibitCategory, ExhibitCategory, ExhibitCategory, ExhibitCategory>, IExhibitCategoryService
    {
        public ExhibitCategoryService(IExhibitCategoryRepository repository, IMapper mapper)
            :base(repository, mapper)
        {

        }
    }
}
