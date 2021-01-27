namespace HotelReservationLibrary.Services.Interfaces
{
    public interface IHotelProcessor
    {
        void SetSizeOfHotel(int size);
        bool ValidateHotelSize(int size);
    }
}
