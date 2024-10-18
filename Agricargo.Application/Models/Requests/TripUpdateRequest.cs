using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.Requests
{
    public class TripUpdateRequest
    {
        public string? Origin { get; set; }
        public string? Destiny { get; set; }
        public float Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArriveDate { get; set; }
    }
}
