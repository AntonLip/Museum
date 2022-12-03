using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces.Service;
using Museum.Models.Interfaces.Repository;

namespace Museum.Services
{
    public class ExhibitService : BaseService<Exhibit, ExhibitDto, AddExhibitDto, AddExhibitDto>, IExhibitService
    {
        private readonly IExhibitRepository _exhibitRepository;
        public ExhibitService(IExhibitRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
           _exhibitRepository = repository;
        }

        public List<ExhibitDto> GetByCategoryId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var items = _repository.GetWithInclude(p => p.CategoryId == id );

            return items is null ? throw new ArgumentNullException(nameof(items)) : _mapper.Map<List<ExhibitDto>>(items);
        }

        public async Task<AddExhibitDto> GetByIdForUpdateAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var item = await _exhibitRepository.GetByIdAsync(id);
            return item is null ? throw new ArgumentNullException(nameof(item)) : _mapper.Map<AddExhibitDto>(item);
        }

        public List<ExhibitDto> GetByPartName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var items = _exhibitRepository.GetByPartName(name);

            return items is null ? throw new ArgumentNullException(nameof(items)) : _mapper.Map<List<ExhibitDto>>(items);
        }
    }
}
