using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Services.Interfaces;

namespace HotelReservationLibrary.Services
{
    public class HotelProcessor : IHotelProcessor
    {
        #region - Consts -

        public const int MAX_SIZE_OF_HOTEL = 1000;

        #endregion

        #region - Fields -

        private readonly IHotelRepository hotelRepository;

        #endregion

        #region - Constructors -

        public HotelProcessor(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        #endregion

        #region - Public Methods -

        public void SetHotelSize(int size)
        {
            hotelRepository.AddHotelRooms(size);
        }

        public bool ValidateHotelSize(int size)
        {
            return size <= MAX_SIZE_OF_HOTEL && size > 0;
        }

        #endregion
    }
}
