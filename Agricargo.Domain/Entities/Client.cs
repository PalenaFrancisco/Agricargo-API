

namespace Agricargo.Domain.Entities;

    public class Client : User
    {

        public List<Reservation>? Reservations { get; set; }


    }

