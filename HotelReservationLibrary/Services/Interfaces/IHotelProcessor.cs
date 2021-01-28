namespace HotelReservationLibrary.Services.Interfaces
{
    public interface IHotelProcessor
    {
        /// <summary>
        /// Set size of hotel
        /// </summary>
        void SetHotelSize(int size);

        /// <summary>
        /// Validates size of hotel
        /// </summary>
        bool ValidateHotelSize(int size);
    }
}
