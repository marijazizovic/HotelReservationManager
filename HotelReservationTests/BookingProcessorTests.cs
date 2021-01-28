using HotelReservationLibrary.DataAccess;
using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Services;
using HotelReservationLibrary.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HotelReservationTests
{
    [TestClass]
    public class BookingProcessorTests
    {
        IBookingProcessor bookingProcessor;
        IHotelProcessor hotelProcessor;

        [TestInitialize]
        public void TestInitialize()
        {
            IReservationRepository reservationRepository = new ReservationRepository();
            IHotelRepository hotelRepository = new HotelRepository();
            bookingProcessor = new BookingProcessor(reservationRepository, hotelRepository);
            hotelProcessor = new HotelProcessor(hotelRepository);
        }

        [TestMethod]
        public void CheckIn_CheckInDayOutsidePeriod()
        {
            // Arrange
            int size = 1;
            int checkIn = -4;
            int checkOut = 2;
            string expected = "Decline";           
            hotelProcessor.SetHotelSize(size);

            // Act
            var actual = bookingProcessor.CheckIn(checkIn, checkOut).ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIn_CheckOutDayOutsidePeriod()
        {
            // Arrange
            int size = 1;
            int checkIn = 200;
            int checkOut = 400;
            string expected = "Decline";
            hotelProcessor.SetHotelSize(size);

            // Act
            var actual = bookingProcessor.CheckIn(checkIn, checkOut).ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIn_AllAccepted()
        {
            // Arrange
            int size = 3;
            var checkInOutList = new List<(int, int)>
            {
                 (0, 5),
                 (7, 13),
                 (3, 9),
                 (5, 7),
                 (6, 6),
                 (0, 4)
            };

            List<string> expected = new List<string>
            {
                "Accept",
                "Accept",
                "Accept",
                "Accept",
                "Accept",
                "Accept"
            };

            hotelProcessor.SetHotelSize(size);
            List<string> actual = new List<string>();

            // Act
            foreach (var item in checkInOutList)
            {
                actual.Add(bookingProcessor.CheckIn(item.Item1, item.Item2).ToString());
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIn_LastDeclined()
        {
            // Arrange
            int size = 3;
            var checkInOutList = new List<(int, int)>
            {
                 (1, 3),
                 (2, 5),
                 (1, 9),
                 (0, 15)
            };

            List<string> expected = new List<string>
            {
                "Accept",
                "Accept",
                "Accept",
                "Decline"
            };

            hotelProcessor.SetHotelSize(size);
            List<string> actual = new List<string>();

            // Act
            foreach (var item in checkInOutList)
            {
                actual.Add(bookingProcessor.CheckIn(item.Item1, item.Item2).ToString());
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIn_AcceptedAfterDecline()
        {
            // Arrange
            int size = 3;
            var checkInOutList = new List<(int, int)>
            {
                 (1, 3),
                 (0, 15),
                 (1, 9),
                 (2, 5),
                 (4, 9)
            };

            List<string> expected = new List<string>
            {
                "Accept",
                "Accept",
                "Accept",
                "Decline",
                "Accept"
            };
           
            hotelProcessor.SetHotelSize(size);
            List<string> actual = new List<string>();

            // Act
            foreach (var item in checkInOutList)
            {
                actual.Add(bookingProcessor.CheckIn(item.Item1, item.Item2).ToString());
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIn_ComplexRequests()
        {
            // Arrange
            int size = 2;
            var checkInOutList = new List<(int, int)>
            {
                 (1, 3),
                 (0, 4),
                 (2, 3),
                 (5, 5),
                 (4, 10),
                 (10, 10),
                 (6, 7),
                 (8, 10),
                 (8, 9)
            };

            List<string> expected = new List<string>
            {
                "Accept",
                "Accept",
                "Decline",
                "Accept",
                "Decline",
                "Accept",
                "Accept",
                "Accept",
                "Accept"
            };
            
            hotelProcessor.SetHotelSize(size);
            List<string> actual = new List<string>();

            // Act
            foreach (var item in checkInOutList)
            {
                actual.Add(bookingProcessor.CheckIn(item.Item1, item.Item2).ToString());
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
