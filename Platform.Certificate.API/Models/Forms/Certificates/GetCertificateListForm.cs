using Platform.Certificate.API.Common.Bases;
using Platform.Certificate.API.Common.Enums;

namespace Platform.Certificate.API.Models.Forms.Certificates
{
    public class GetCertificateListForm : BaseGetListForm
    {
        public string? Number { get; set; }
        public string? Country { get; set; }
        public CertificateStateEnum? State { get; set; }
    }
}