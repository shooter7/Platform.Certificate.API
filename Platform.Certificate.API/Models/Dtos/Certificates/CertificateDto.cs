using Platform.Certificate.API.Common.Enums;
using Platform.Certificate.API.Models.Objects;

namespace Platform.Certificate.API.Models.Dtos.Certificates
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public Agent Importer { get; set; }
        public Agent Exporter { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public Guid PublicId { get; set; }
        public CertificateStateEnum State { get; set; }
        public string Path { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}