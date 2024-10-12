using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;

namespace Agricargo.Application.Interfaces
{
    public interface IUserService
    {
        public bool RegisterUser(UserCreateRequest userRequest, Guid? creatorId = null);

        public bool UpdateUser(User user);

        public bool DeleteUser(User user);


    }
}
