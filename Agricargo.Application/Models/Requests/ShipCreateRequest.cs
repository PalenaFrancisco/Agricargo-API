using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.Requests
{
    public class ShipCreateRequest
    {
        public string? TypeShip { get; set; }
        public float Capacity { get; set; }
        public string? Captain { get; set; }
        public bool Available { get; set; } = true;

        public Guid? CompanyId { get; set; }
    }
}
