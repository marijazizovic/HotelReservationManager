using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Enums;
using HotelReservationLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationLibrary.Services
{
    public class BookingProcessor : IBookingProcessor
    {
        public const int ReservationPeriod = 364;

        private readonly IHotelRepository hotelRepository;
        private readonly IReservationRepository reservationRepository;

        public BookingProcessor(IReservationRepository reservationRepository, IHotelRepository hotelRepository)
        {
            this.reservationRepository = reservationRepository;
            this.hotelRepository = hotelRepository;
        }

        public BookingResult CheckIn(int checkInDay, int checkOutDay)
        {
            if (!ValidateDays(checkInDay, checkOutDay))
                return BookingResult.Decline;

            try
            {
                var reservations = reservationRepository.GetReservations();
                var reserevedRooms = reservations
                    .Where(r => (checkInDay >= r.CheckInDay && checkOutDay <= r.CheckOutDay)
                             || (checkInDay <= r.CheckInDay && checkOutDay >= r.CheckOutDay)
                             || (checkInDay <= r.CheckOutDay && checkOutDay >= r.CheckOutDay)
                             || (checkOutDay >= r.CheckInDay && checkInDay <= r.CheckInDay))
                    .Select(r => r.RoomNumber)
                    .Distinct()
                    .ToList();
                
                var availableRooms = hotelRepository.GetHotelRooms().Except(reserevedRooms);
                var optimalRoom = GetOptimalRoom(availableRooms);

                if (optimalRoom == 0)
                    return BookingResult.Decline;

                reservationRepository.AddReservation(checkInDay, checkOutDay, optimalRoom);
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

        private bool ValidateDays(int checkInDay, int checkOutDay)
        {
            return checkInDay >= 0 && checkOutDay <= ReservationPeriod && checkInDay <= checkOutDay;
        }
    }
}
