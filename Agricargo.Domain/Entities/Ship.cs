

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agricargo.Domain.Entities;

public class Ship
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShipId { get; set; }
    [Required]
    public string? TypeShip { get; set; }
    [Required]
    public float Capacity { get; set; }
    [Required]
    public string? Captain { get; set; }
    public bool Available { get; set; } = true;
}
