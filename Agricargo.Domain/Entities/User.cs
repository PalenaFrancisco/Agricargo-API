

namespace Agricargo.Domain.Entities;

public abstract class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!; 
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? TypeUser { get; set; }
        
}

