using HotelReservationLibrary.Models;
using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetReservations();
        void AddReservation(int checkIn, int checkOut, int roomNumber);
    }
}
