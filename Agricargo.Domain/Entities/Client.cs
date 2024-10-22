

namespace Agricargo.Domain.Entities;

    public class Client : User
    {

        public List<Reservation>? Reservations { get; set; }

        public List<Favorite>? Favorites { get; set; }


    }

