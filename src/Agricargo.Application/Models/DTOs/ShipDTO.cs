using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.DTOs
{
    public class ShipDTO
    {
        public int Id { get; set; }
        public string? TypeShip { get; set; }
        public float Capacity { get; set; }
        public string? Captain { get; set; }
        public string? ShipPlate { get; set; }
    }
}
