using HotelReservationLibrary.Models;
using System.Collections.Generic;

namespace HotelReservationLibrary
{
    public static class DataAccess
    {
        public static IEnumerable<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            return reservations;
        }

        public static void AddReservation(int checkIn, int checkOut, int roomNumber, List<Reservation> reservations)
        {
            reservations.Add(new Reservation { CheckInDay = checkIn, CheckOutDay = checkOut, RoomNumber = roomNumber});
        }
    }
}
