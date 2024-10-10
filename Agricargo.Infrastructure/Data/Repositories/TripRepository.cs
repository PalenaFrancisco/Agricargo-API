using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricargo.Infrastructure.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationDbContext context) : base(context) { }
    }
}
