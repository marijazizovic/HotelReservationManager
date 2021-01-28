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
        #region - Consts -

        public const int RESERVATION_PERIOD = 364;

        #endregion

        #region - Fields -

        private readonly IHotelRepository hotelRepository;
        private readonly IReservationRepository reservationRepository;

        #endregion

        #region - Constructors -

        public BookingProcessor(IReservationRepository reservationRepository, IHotelRepository hotelRepository)
        {
            this.reservationRepository = reservationRepository;
            this.hotelRepository = hotelRepository;
        }

        #endregion

        #region - Public Methods -

        public BookingResult CheckIn(int checkInDay, int checkOutDay)
        {
            if (!ValidateDays(checkInDay, checkOutDay))
                return BookingResult.Decline;

            try
            {
                var allReservations = reservationRepository.GetReservations();
                var reserevedRooms = allReservations
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

        #endregion

        #region - Private Methods -

        /// <summary>
        /// TODO: Add optimization for room reservation
        /// </summary>
        private int GetOptimalRoom(IEnumerable<int> availableRooms)
        {
            return availableRooms.FirstOrDefault();
        }

        /// <summary>
        /// Validates checkInDay and checkOutDay of reservation
        /// </summary>
        private bool ValidateDays(int checkInDay, int checkOutDay)
        {
            return checkInDay >= 0 && checkOutDay <= RESERVATION_PERIOD && checkInDay <= checkOutDay;
        }

        #endregion
    }
}
