using System.ComponentModel.DataAnnotations;
using Platform.Certificate.API.Models.Dbs;
using Platform.Certificate.API.Models.Objects;

namespace Platform.Certificate.API.Models.Forms.Certificates
{
    public class CertificateForm
    {
        [Required] public Agent Importer { get; set; }
        [Required] public Agent Exporter { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        [Required] public string Code { get; set; }
        [Required] public IFormFile File { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}