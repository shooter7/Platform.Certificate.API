using Platform.Certificate.API.Common.Bases;

namespace Platform.Certificate.API.Models.Forms.Certificates
{
    public class GetCertificateListForm : BaseGetListForm
    {
        public string? Number { get; set; }
    }
}