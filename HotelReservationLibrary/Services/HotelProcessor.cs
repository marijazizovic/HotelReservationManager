using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Services.Interfaces;

namespace HotelReservationLibrary.Services
{
    public class HotelProcessor : IHotelProcessor
    {
        public const int MAX_SIZE_OF_HOTEL = 1000;

        private readonly IHotelRepository hotelRepository;

        public HotelProcessor(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public void SetSizeOfHotel(int size)
        {
            hotelRepository.AddHotelRooms(size);
        }

        public bool ValidateHotelSize(int size)
        {
            return size <= MAX_SIZE_OF_HOTEL && size > 0;
        }
    }
}
