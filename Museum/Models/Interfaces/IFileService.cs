namespace Museum.Models.Interfaces
{
    public interface IFileService
    {
        string UploadFile(IFormFile model);
        bool RemoveFile(string filePath);
    }
}