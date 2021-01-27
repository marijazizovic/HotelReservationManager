using HotelReservationLibrary.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationLibrary.DataAccess
{
    public class HotelRepository : IHotelRepository
    {
        private List<int> hotelRooms;

        public HotelRepository()
        {
            hotelRooms = new List<int>();
        }

        public IEnumerable<int> GetHotelRooms()
        {
            return hotelRooms;
        }

        public void AddHotelRooms(int size)
        {
            hotelRooms = Enumerable.Range(1, size).ToList();
        }
    }
}
