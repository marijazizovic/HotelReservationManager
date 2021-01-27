using HotelReservationLibrary.Enums;

namespace HotelReservationLibrary.Services.Interfaces
{
    public interface IBookingProcessor
    {
        BookingResult CheckIn(int checkInDay, int checkOutDay);        
    }
}
