namespace Reservations
{
    public class HotelService: IHotelService
    {
        public IVacationPart MakeBooking(HotelInfo hotel, DateTime checkin,
                                         DateTime checkout)
        {
            Console.WriteLine("Waiting for remote hotel booking service to respond...");
            System.Threading.Thread.Sleep(300); // Waiting for remote service
            Console.WriteLine($"Booking hotel {hotel} {checkin:dd-MMM-yyyy} - {checkout:dd-MMMM-yyyy}\n");
            return new DummyVacationPart("Hotel " + hotel.ToString());
        }
    }
}
