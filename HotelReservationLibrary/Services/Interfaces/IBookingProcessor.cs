using HotelReservationLibrary.Enums;

namespace HotelReservationLibrary.Services.Interfaces
{
    public interface IBookingProcessor
    {
        /// <summary>
        /// Adds a new reservation if there is a free room
        /// </summary>
        BookingResult CheckIn(int checkInDay, int checkOutDay);        
    }
}
