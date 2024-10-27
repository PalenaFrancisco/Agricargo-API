

namespace Agricargo.Domain.Entities;

public class Trip
{
    public int Id { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public float Price { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }
    public string? TripState { get; set; }

    public float AvailableCapacity { get; set; }

    public int ShipId { get; set; }
    public Ship? Ship { get; set; }
}
