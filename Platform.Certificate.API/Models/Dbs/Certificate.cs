using Platform.Certificate.API.Common.Bases;

namespace Platform.Certificate.API.Models.Dbs;

public class Certificate : BaseEntity<Guid>
{
    public string Number { get; set; }
    public string Path { get; set; }
}