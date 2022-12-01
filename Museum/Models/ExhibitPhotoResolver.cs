using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces;
using Museum.Models.Interfaces.Repository;

namespace Museum.Models
{
    internal class ExhibitPhotoResolver : IValueResolver<AddExhibitDto, Exhibit, string>
    {
        private readonly IFileService _fileService;
        private readonly IExhibitRepository _exhibitRepository;
        public ExhibitPhotoResolver(IFileService fileService, IExhibitRepository exhibitRepository)
        {
            _fileService = fileService;
            _exhibitRepository = exhibitRepository;
        }
        public string Resolve(AddExhibitDto source, Exhibit destination, string destMember, ResolutionContext context)
        {
            try
            {
                var item = _exhibitRepository.GetByIdAsync(source.Id).Result;
                if (item is null)
                {
                    var path = _fileService.UploadFile(source.PhotoPath);
                    destination.IconPath = _fileService.UploadFile(source.IconPath);
                    return path;
                }
                else
                {
                    if (_fileService.RemoveFile(item.PhotoPath))
                    {
                        var path = _fileService.UploadFile(source.PhotoPath);
                        if (_fileService.RemoveFile(item.IconPath))
                        {
                            destination.IconPath = _fileService.UploadFile(source.IconPath);
                        }
                        return path;
                    }
                    return " ";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}