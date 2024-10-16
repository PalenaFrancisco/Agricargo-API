using Agricargo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Interfaces
{
    public interface IAuthService
    {
        public string Login(string email, string password);

        public bool Register(string email, string password, string role, string name, Guid? sysAdminId = null, string? companyName = null);

        public User CreateUserByRole(string email, string password, string role, string name, string companyName = null);
    }
}
