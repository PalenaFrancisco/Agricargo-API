using Agricargo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Interfaces
{
    public interface IAuthService
    {
        public string Login(string email, string password);

        public bool Register(ClaimsPrincipal user, string email, string password, string role, string name, string? companyName = null);

        public User CreateUserByRole(string email, string password, string role, string name, string companyName = null);

    }
}
