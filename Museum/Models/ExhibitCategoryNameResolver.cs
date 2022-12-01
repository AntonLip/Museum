using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces.Repository;

namespace Museum.Models
{
    internal class ExhibitCategoryNameResolver : IValueResolver<Exhibit, ExhibitDto, string>
    {
        private readonly IExhibitCategoryRepository _exhibitCategoryRepository;
        public ExhibitCategoryNameResolver(IExhibitCategoryRepository exhibitCategoryRepository)
        {
            _exhibitCategoryRepository = exhibitCategoryRepository;
        }

        public  string Resolve(Exhibit source, ExhibitDto destination, string destMember, ResolutionContext context)
        {
            try
            {
                var item = _exhibitCategoryRepository.GetByIdAsync(source.CategoryId).Result;
                destMember = item.Name;
                return item.Name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}