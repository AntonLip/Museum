using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Exhibit, ExhibitDto>()
                .ForMember(dest => dest.ExhibitCategoryName,
                opt => 
                {
                    opt.MapFrom<ExhibitCategoryNameResolver>();
                });
            CreateMap<AddExhibitDto, Exhibit>()
                .ForMember(dest => dest.PhotoPath,
                opt =>
                {
                    opt.MapFrom<ExhibitPhotoResolver>();
                });
            CreateMap<Exhibit, AddExhibitDto>()
                .ForMember(x => x.IconPath, opt => opt.Ignore())
                .ForMember(x => x.PhotoPath, opt => opt.Ignore());
        }
    }
}
