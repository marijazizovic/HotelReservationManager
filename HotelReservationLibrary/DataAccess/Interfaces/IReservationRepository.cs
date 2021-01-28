using HotelReservationLibrary.Models;
using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess.Interfaces
{
    public interface IReservationRepository
    {
        /// <summary>
        /// Gets all hotel reservations
        /// </summary>
        IEnumerable<Reservation> GetReservations();

        /// <summary>
        /// Adds new reservation in room for a certain period 
        /// </summary>
        void AddReservation(int checkIn, int checkOut, int roomNumber);
    }
}
