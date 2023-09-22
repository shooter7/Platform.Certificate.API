using System.ComponentModel.DataAnnotations;

namespace Platform.Certificate.API.Models.Forms.Certificates
{
    public class CertificateForm
    {
        [Required] public string Number { get; set; }
        [Required] public IFormFile File { get; set; }
    }
}