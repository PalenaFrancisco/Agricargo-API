

namespace Agricargo.Domain.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public float PurchasePrice { get; set; }
    public float PurchaseAmount { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }
    public string? ReservationStatus { get; set; }

    public int TripId { get; set; }
    public Trip? Trip { get; set; }

    public Guid ClientId { get; set; } 
    public Client? Client { get; set; }
}
