

using Agricargo.Domain.Entities;

namespace Agricargo.Application.Models.Requests;

public class ReservationRequest
{
    public int Id { get; set; }
    public float PurchaseAmount { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }
    public string? ReservationStatus { get; set; }

    public int TripId { get; set; }

    public Guid ClientId { get; set; }

}
