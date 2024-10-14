using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByEmail(string email);

        public bool IsSuperAdmin(Guid userId);
    }
}
