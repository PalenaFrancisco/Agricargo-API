

namespace Agricargo.Domain.Entities;

public abstract class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? TypeUser { get; set; }
    public string Password { get; set; }
}

