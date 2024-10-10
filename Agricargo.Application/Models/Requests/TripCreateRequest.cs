using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.Requests
{
    public class TripCreateRequest
    {
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public float Price { get; set; }
        public string? TripState { get; set; }
        public bool IsFullCapacity { get; set; } = false;
        public int ShipId { get; set; }
    }
}
