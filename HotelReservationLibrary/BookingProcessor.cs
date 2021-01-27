using HotelReservationLibrary.Enums;
using HotelReservationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationLibrary
{
    public sealed class BookingProcessor
    {
        public const int ReservationPeriod = 364;

        public const int MaxSizeOfHotel = 1000;

        private static BookingProcessor _instance = null;

        private static readonly object _instanceLock = new object();

        public int[] HotelRooms { get; set; }

        private List<Reservation> reservations;

        private BookingProcessor()
        {
            reservations = DataAccess.GetReservations().ToList();
        }
        public static BookingProcessor GetInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new BookingProcessor();
                    }
                    return _instance;
                }
            }
        }

        public BookingResult CheckIn(int checkInDay, int checkOutDay)
        {
            if (!ValidateDays(checkInDay, checkOutDay))
                return BookingResult.Decline;

            try
            {
                var reserevedRooms = reservations.Where(r => (checkInDay >= r.CheckInDay && checkOutDay <= r.CheckOutDay)
                                                          || (checkInDay <= r.CheckInDay && checkOutDay >= r.CheckOutDay)
                                                          || (checkInDay <= r.CheckOutDay && checkOutDay >= r.CheckOutDay)
                                                          || (checkOutDay >= r.CheckInDay && checkInDay <= r.CheckInDay))
                                                 .Select(r => r.RoomNumber).Distinct().ToList();
                
                var availableRooms = HotelRooms.Except(reserevedRooms);
                var optimalRoom = GetOptimalRoom(availableRooms);

                if (optimalRoom == 0)
                    return BookingResult.Decline;

                DataAccess.AddReservation(checkInDay, checkOutDay, optimalRoom, reservations);
                return BookingResult.Accept;
 
            }
            catch (Exception)
            {
                return BookingResult.Decline;
            }           
        }

        private int GetOptimalRoom(IEnumerable<int> availableRooms)
        {
            //TODO: Add optimization 
            return availableRooms.FirstOrDefault();
        }

        public void SetSizeOfHotel(int size) => HotelRooms = Enumerable.Range(1, size).ToArray();

        public bool ValidateDays(int checkInDay, int checkOutDay) => checkInDay >= 0 && checkOutDay <= ReservationPeriod && checkInDay <= checkOutDay;

        public bool ValidateHotelSize(int size) => size <= MaxSizeOfHotel && size > 0;
    }
}
