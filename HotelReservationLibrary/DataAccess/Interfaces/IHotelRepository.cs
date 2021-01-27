using System.Collections.Generic;

namespace HotelReservationLibrary.DataAccess.Interfaces
{
    public interface IHotelRepository
    {
        IEnumerable<int> GetHotelRooms();
        void AddHotelRooms(int size);       
    }
}
