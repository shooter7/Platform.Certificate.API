using Platform.Certificate.API.Common.Enums;

namespace Platform.Certificate.API.Models.Dtos.Certificates
{
    public class CertificateDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public CertificateStateEnum State { get; set; }
        public string Path { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}