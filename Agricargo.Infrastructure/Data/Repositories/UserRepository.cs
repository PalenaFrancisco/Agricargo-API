using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricargo.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Método para obtener todos los administradores
        public IEnumerable<Company> GetAllAdmins()
        {
            return _context.Users.OfType<Company>().ToList();
        }

        // Método para obtener todos los clientes
        public IEnumerable<Client> GetAllClients()
        {
            return _context.Users.OfType<Client>().ToList();
        }

        // Método para obtener un usuario por correo electrónico
        public  User? GetUserByEmail(string email)
        {
            return  _context.Users.FirstOrDefault(u => u.Email == email);
        }

        // Método para verificar si un usuario es un SuperAdmin
        public  bool IsSuperAdmin(Guid userId)
        {
            var user =  _context.Users.Find(userId);
            return user is SuperAdmin; // Verifica si es un SuperAdmin
        }

        public User GetUserById(Guid id)
        {
            var user = _context.Users.Find(id);
            return user;
        }
    }
}
