using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models.Interfaces
{
    public interface IFileService
    {
        string UploadFile(IFormFile model);
        bool RemoveFile(string filePath);
        FileDto GetTicketInPDF(Ticket item);
    }
}