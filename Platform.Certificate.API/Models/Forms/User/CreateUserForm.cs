using System.ComponentModel.DataAnnotations;

namespace Platform.Certificate.API.Models.Forms.User
{
    public class CreateUserForm
    {
        [Required] public string Username { get; set; }
        [Required] public string Fullname { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required] public string Role { get; set; }
    }
}