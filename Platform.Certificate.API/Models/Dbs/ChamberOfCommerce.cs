using Platform.Certificate.API.Common.Bases;

namespace Platform.Certificate.API.Models.Dbs;

public class ChamberOfCommerce:BaseEntity<int>
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string Code { get; set; }
}