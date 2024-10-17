

namespace Agricargo.Domain.Entities;

public class Company : User
{
    public string CompanyName { get; set; } = default!;
    public List<Ship> Ships { get; set; }
}
