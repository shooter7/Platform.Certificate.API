using Platform.Certificate.API.Common.Bases;

namespace Platform.Certificate.API.Models.Forms.User
{
    public class GetUserListForm:BaseGetListForm
    {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
    }
}
