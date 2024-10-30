namespace Agricargo.Application.Models.DTOs;

public class TripDTO
{
    public int Id { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public float PricePerTon { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }
    public float Capacity { get; set; }
}