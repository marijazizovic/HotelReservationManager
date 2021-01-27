using HotelReservationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelReservationLibrary
{
    public static class DataAccess
    {
        public static IEnumerable<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            return reservations;
        }

        public static void AddReservation(int checkIn, int checkOut, Dictionary<int, List<Reservation>> reservationsByRoom, List<Reservation> reservations, int[] hotelRooms)
        {
            reservations.Add(new Reservation { CheckIn = checkIn, CheckOut = checkOut, RoomNumber = hotelRooms.Except(reservationsByRoom.Keys).First() });
        }
    }
}
