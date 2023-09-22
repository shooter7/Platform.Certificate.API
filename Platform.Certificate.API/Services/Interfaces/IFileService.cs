namespace Platform.Certificate.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
        bool DeleteFile(string path);
    }
}
