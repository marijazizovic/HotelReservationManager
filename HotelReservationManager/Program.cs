using HotelReservationLibrary;
using HotelReservationLibrary.Enums;
using System;

namespace HotelReservationManager
{
    class Program
    {
        static BookingProcessor bookingProcessor;
        static void Main(string[] args)
        {
            bookingProcessor = BookingProcessor.GetInstance;

            Console.Write("Enter size of hotel:");
            string size = Console.ReadLine();

            int sizeOfHotel;
            while (!int.TryParse(size, out sizeOfHotel) || !bookingProcessor.ValidateHotelSize(sizeOfHotel))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid number given:");
                Console.Write("Enter size of hotel:");
                size = Console.ReadLine();
            }

            bookingProcessor.SetSizeOfHotel(sizeOfHotel);

            MakeReservations();
        }

        private static void MakeReservations()
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
                    checkInInput = Console.ReadLine();
                }

                Book(checkIn, checkOut);

                Console.Write("Do you want to book more (yes/no): ");
                enterMoreReservations = Console.ReadLine();

            } while (enterMoreReservations.ToLower() == "yes");
        }

        private static void Book(int checkIn, int checkOut)
        {
            BookingResult result = bookingProcessor.CheckIn(checkIn, checkOut);
            Console.WriteLine(result.ToString());
        }
    }
}
