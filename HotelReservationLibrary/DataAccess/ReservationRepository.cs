using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Models;
using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> reservations;

        public ReservationRepository()
        {
            reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return reservations;
        }

        public void AddReservation(int checkIn, int checkOut, int roomNumber)
        {
            reservations.Add(new Reservation { CheckInDay = checkIn, CheckOutDay = checkOut, RoomNumber = roomNumber });
        }
    }
}
