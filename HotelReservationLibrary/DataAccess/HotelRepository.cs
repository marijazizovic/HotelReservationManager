using HotelReservationLibrary.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationLibrary.DataAccess
{
    public class HotelRepository : IHotelRepository
    {
        #region - Fields -

        private List<int> hotelRooms;

        #endregion

        #region - Constructors -

        public HotelRepository()
        {
            hotelRooms = new List<int>();
        }

        #endregion

        #region - Public Methods -

        public IEnumerable<int> GetHotelRooms()
        {
            return hotelRooms;
        }

        public void AddHotelRooms(int size)
        {
            hotelRooms = Enumerable.Range(1, size).ToList();
        }

        #endregion
    }
}
