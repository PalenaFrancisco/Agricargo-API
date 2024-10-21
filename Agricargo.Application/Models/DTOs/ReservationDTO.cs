

namespace Agricargo.Application.Models.DTOs;

public class ReservationDTO
{
    public int Id { get; set; }
    public string? Trip { get; set; }
    public DateTime Date { get; set; }
    public float Price { get; set; }
    public string? Status { get; set; }
}
