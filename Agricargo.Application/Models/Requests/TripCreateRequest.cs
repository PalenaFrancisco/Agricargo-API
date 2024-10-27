

namespace Agricargo.Application.Models.Requests;

public class TripCreateRequest
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public float Price { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }

    public int ShipId { get; set; }
}
