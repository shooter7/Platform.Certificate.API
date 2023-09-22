using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Platform.Certificate.API.Common.Helpers;

namespace Platform.Certificate.API.Common.Bases
{
    public class BaseEntity<TId> : IEntity where TId : struct
    {
        [Key] public TId Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore] public bool IsDeleted { get; set; }
    }
}