using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agricargo.Domain.Entities;

public abstract class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
    public string Name { get; set; } 

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^\+?\d{10,15}$",ErrorMessage = "Phone number must be between 10 and 15 digits and can start with a '+'.")]
    public string? Phone { get; set; }

    [Required]
    [RegularExpression(@"^(Admin|Client|SuperAdmin)$", ErrorMessage = "TypeUser must be 'Admin', 'Client', or 'SuperAdmin'.")]
    public string? TypeUser { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z\d]{8,}$")]
    public string Password { get; set; }
}

