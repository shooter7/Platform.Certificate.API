using System.ComponentModel.DataAnnotations;

namespace Platform.Certificate.API.Models.Forms.User
{
    public class UpdatePasswordForm
    {
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string NewPassword { get; set; }
    }
}