using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Application.Interfaces
{
    public interface IUserService
    {
        public User GetUserInfo(ClaimsPrincipal user);
        public void UpdateUser([FromBody] UpdateUserRequest userUpdate, ClaimsPrincipal user);

        public void DeleteUser(ClaimsPrincipal user);
    }
}
