using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Models.Requests
{
    public class UserCreateRequest
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string? CompanyName { get; set; }


        public UserCreateRequest(string name, string email, string password, string role, string? companyName)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            CompanyName = companyName;
        }
        
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                return false;
            }

            if (Role != "Client" && Role != "Admin" && Role != "SuperAdmin")
            {
                return false;
            }

            return true;
        }
    }
}
