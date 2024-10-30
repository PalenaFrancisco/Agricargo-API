using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agricargo.Domain.Entities;

public class Trip
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Origin { get; set; }

    [Required]
    [StringLength(50)]
    public string? Destination { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public float Price { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ArriveDate { get; set; }

    public string? TripState { get; set; }
    public float AvailableCapacity { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int ShipId { get; set; }
    public Ship? Ship { get; set; }
}
