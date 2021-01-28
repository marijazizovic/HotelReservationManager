using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Models;
using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        #region - Fields -

        private List<Reservation> reservations;

        #endregion

        #region - Constructors -

        public ReservationRepository()
        {
            reservations = new List<Reservation>();
        }

        #endregion

        #region - Public Methods -

        public IEnumerable<Reservation> GetReservations()
        {
            return reservations;
        }

        public void AddReservation(int checkInDay, int checkOutDay, int roomNumber)
        {
            reservations.Add(new Reservation { CheckInDay = checkInDay, CheckOutDay = checkOutDay, RoomNumber = roomNumber });
        }

        #endregion
    }
}
