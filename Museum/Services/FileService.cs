using Museum.Models.DbModels;
using Museum.Models.DtoModels;
using Museum.Models.Interfaces;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Museum.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _appEnvironment;
        public FileService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public FileDto GetTicketInPDF(Ticket item)
        {
            FileDto file = new FileDto();
            file.FileType = "application/pdf";
            file.FileName = $"Билет {item.LastName} {item.FirstName} {item.Id}.pdf";
            var doc = new PdfDocument();
            doc.Info.Title = "Виртуальный музей";
            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 20);
            gfx.DrawString($"Билет в музей", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100),  new XPoint(500,100));
            gfx.DrawString($"Фамилия {item.LastName}", font, XBrushes.Black, new XPoint(100, 150));
            gfx.DrawString($"Имя {item.FirstName}", font, XBrushes.Black, new XPoint(100, 200));
            gfx.DrawString($"Время посещения {item.VisitTime}", font, XBrushes.Black, new XPoint(100, 250));           
            using(MemoryStream stream = new MemoryStream())
            {
                doc.Save(stream);
                file.FileBytes = stream.ToArray();               
            }
            return file;
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
