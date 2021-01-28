using HotelReservationLibrary.Enums;
using HotelReservationLibrary.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace HotelReservationManager
{
    public class App
    {
        #region - Fields -

        private readonly IConfiguration config;
        private readonly IBookingProcessor bookingProcessor;
        private readonly IHotelProcessor hotelProcessor;

        #endregion

        #region - Constructors -

        public App(IConfiguration config, IBookingProcessor bookingProcessor, IHotelProcessor hotelProcessor)
        {
            this.bookingProcessor = bookingProcessor;
            this.hotelProcessor = hotelProcessor;
            this.config = config;
        }

        #endregion

        #region - Public Methods -

        public void Run()
        {
            Console.Write("Enter size of hotel:");
            string size = Console.ReadLine();

            int sizeOfHotel;
            while (!int.TryParse(size, out sizeOfHotel) || !hotelProcessor.ValidateHotelSize(sizeOfHotel))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid number given:");
                Console.Write("Enter size of hotel:");
                size = Console.ReadLine();
            }

            hotelProcessor.SetHotelSize(sizeOfHotel);

            MakeReservations();
        }

        #endregion

        #region - Private Methods -

        private void MakeReservations()
        {
            string enterMoreReservations;
            do
            {
                Console.Write("Enter CHECK IN:");
                string checkInInput = Console.ReadLine();

                int checkIn;
                while (!int.TryParse(checkInInput, out checkIn))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid number given:");
                    Console.Write("Enter CHECK IN:");
                    checkInInput = Console.ReadLine();
                }

                Console.Write("Enter CHECK OUT:");
                string checkOutInput = Console.ReadLine();

                int checkOut;
                while (!int.TryParse(checkOutInput, out checkOut))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid number given:");
                    Console.Write("Enter CHECK OUT:");
                    checkOutInput = Console.ReadLine();
                }

                BookRoom(checkIn, checkOut);

                Console.Write("Do you want to book more (yes/no): ");
                enterMoreReservations = Console.ReadLine();

            } while (enterMoreReservations.ToLower() == "yes");
        }

        private void BookRoom(int checkIn, int checkOut)
        {
            BookingResult result = bookingProcessor.CheckIn(checkIn, checkOut);
            Console.WriteLine(result.ToString());
        }

        #endregion
    }
}
