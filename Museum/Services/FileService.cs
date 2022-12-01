using Museum.Models.Interfaces;

namespace Museum.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _appEnvironment;
        public FileService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public bool RemoveFile(string filePath)
        {
            if (filePath != null)
            {
                string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "images");
                string file = Path.Combine(uploadsFolder, filePath);
                FileInfo fileInf = new FileInfo(file);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                    return true;
                }
            }
            return false;
        }
        public string UploadFile(IFormFile model)
        {
            string uniqueFileName = null;

            if (model != null)
            {
                string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }
    }
}
