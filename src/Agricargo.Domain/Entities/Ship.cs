

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Agricargo.Domain.Entities;

public class Ship
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? TypeShip { get; set; }

    [Required]
    [Range(1,int.MaxValue)]
    [RegularExpression("[-+]?[1-9]*\\.?[0-9]+")]
    public float Capacity { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$")]
    public string? Captain { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z\d]{8,}$")]
    public string? ShipPlate { get; set; }

    [JsonIgnore]
    public List<Trip>? Trips { get; set; }

    //[Required]
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }

    public string AvailabilityStatus
    {
        get
        {
            var currentDate = DateTime.Now;

            // Verificar si hay algún viaje en curso
            var tripInProgress = Trips?.Any(trip => trip.DepartureDate <= currentDate && trip.ArriveDate >= currentDate) ?? false;

            return tripInProgress ? "Ocupado" : "Disponible";
        }
    }
}
