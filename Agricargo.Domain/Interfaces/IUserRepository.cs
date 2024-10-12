

using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserById(Guid id);
        User? GetUserByEmail(string email);
        bool IsSuperAdmin(Guid userId);
        IEnumerable<Client> GetAllClients();
        IEnumerable<Company> GetAllAdmins();
    }
}
