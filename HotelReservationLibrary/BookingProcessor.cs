using HotelReservationLibrary.Enums;
using HotelReservationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelReservationLibrary
{
    public class BookingProcessor
    {
        private static readonly BookingProcessor _instance = new BookingProcessor();

        public int[] HotelRooms { get; set; }

        public int ReservationPeriod { get; set; } = 364;

        public int MaxSizeOfHotel { get; set; } = 1000;

        private List<Reservation> reservations;

        private BookingProcessor()
        {
            reservations = DataAccess.GetReservations().ToList();
        }
        public static BookingProcessor GetInstance()
        {
            return _instance;
        }

        public Response CheckIn(int checkIn, int checkOut)
        {
            if (!ValidateDates(checkIn, checkOut))
                return Response.Decline;

            try
            {
                Dictionary<int, List<Reservation>> reservationsByRoom = reservations.Where(r => (checkIn >= r.CheckIn && checkOut <= r.CheckOut)
                                                                                             || (checkIn <= r.CheckIn && checkOut >= r.CheckOut)
                                                                                             || (checkIn <= r.CheckOut && checkOut >= r.CheckOut)
                                                                                             || (checkOut >= r.CheckIn && checkIn <= r.CheckIn))
                                                                                    .GroupBy(o => o.RoomNumber)
                                                                                    .ToDictionary(g => g.Key, g => g.ToList());

                if (reservationsByRoom.Count < HotelRooms.Length)
                {
                    DataAccess.AddReservation(checkIn, checkOut, reservationsByRoom, reservations, HotelRooms);
                    return Response.Accept;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Response.Decline;
        }

        public void SetSizeOfHotel(int size) => HotelRooms = Enumerable.Range(1, size).ToArray();

        public bool ValidateDates(int checkIn, int checkOut) => checkIn >= 0 && checkOut <= ReservationPeriod && checkIn <= checkOut;

        public bool ValidateHotelSize(int size) => size <= MaxSizeOfHotel && size > 0;
    }
}
