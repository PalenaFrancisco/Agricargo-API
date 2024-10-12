

namespace Agricargo.Domain.Entities;

public class Company : User
{
    public string CompanyName { get; set; } = default!;

    public Company() 
    {
        TypeUser = "Admin";   
    }
}
