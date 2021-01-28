using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess.Interfaces
{
    public interface IHotelRepository
    {
        /// <summary>
        /// Gets all hotel rooms for hotel
        /// </summary>
        IEnumerable<int> GetHotelRooms();

        /// <summary>
        /// Adds rooms to hotel based on size of hotel
        /// </summary>
        void AddHotelRooms(int size);       
    }
}
