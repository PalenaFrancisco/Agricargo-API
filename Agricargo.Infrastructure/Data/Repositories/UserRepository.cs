using Agricargo.Domain.Interfaces;
using Agricargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agricargo.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public bool IsSuperAdmin(Guid userId)
        {
            var user = _context.Users.Find(userId);
            return user is SuperAdmin;
        }
    }
}
