using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess.Interfaces
{
    public interface IHotelRepository
    {
        void AddHotelRooms(int size);
        IEnumerable<int> GetHotelRooms();
    }
}
