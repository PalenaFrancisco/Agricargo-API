using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.Requests
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid? SysId { get; set; } = null;
        public string? CompanyName { get; set; } = null;
    }
}
