using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Platform.Certificate.API.Common.Bases;

namespace Platform.Certificate.API.Models.Dbs
{
    public class User : BaseEntity<int>
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        [JsonIgnore] public string Password { get; set; }
        public string Role { get; set; }
      
    }
}