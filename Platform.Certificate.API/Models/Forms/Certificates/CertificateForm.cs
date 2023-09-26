using System.ComponentModel.DataAnnotations;

namespace Platform.Certificate.API.Models.Forms.Certificates
{
    public class CertificateForm
    {
        [Required] public string Number { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        [Required] public IFormFile File { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}