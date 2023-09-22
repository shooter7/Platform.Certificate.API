using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(_environment.WebRootPath, "Certificate");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, fileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return Path.Combine("Certificate", fileName);
        }

        public bool DeleteFile(string path)
        {
            if (File.Exists(Path.Combine(_environment.WebRootPath, path)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(_environment.WebRootPath, path));
                return true;
            }

            return false;
        }
    }
}