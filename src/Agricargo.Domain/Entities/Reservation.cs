

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agricargo.Domain.Entities;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public float PurchasePrice { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public float PurchaseAmount { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime PurchaseDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ArriveDate { get; set; }

    public string? ReservationStatus { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TripId { get; set; }
    public Trip? Trip { get; set; }

    [Required]
    public Guid ClientId { get; set; } 
    public Client? Client { get; set; }
}
