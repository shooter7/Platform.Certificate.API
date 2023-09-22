using System.ComponentModel.DataAnnotations;

namespace Platform.Certificate.API.Models.Forms.User
{
    public class LoginForm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
