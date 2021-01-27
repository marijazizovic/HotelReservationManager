using HotelReservationLibrary.DataAccess;
using HotelReservationLibrary.DataAccess.Interfaces;
using HotelReservationLibrary.Services;
using HotelReservationLibrary.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace HotelReservationManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddSingleton<IBookingProcessor, BookingProcessor>();
            services.AddSingleton<IHotelProcessor, HotelProcessor>();

            // required to run the application
            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }     
    }
}
