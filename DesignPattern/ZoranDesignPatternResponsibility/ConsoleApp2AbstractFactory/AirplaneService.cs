namespace Reservations
{
    public class AirplaneService: IAirplaneService
    {
        public IVacationPart SelectFlight(string companyName, string origin,
                                          string destination, DateTime travelDate)
        {
            Console.WriteLine("Waiting for air ticketing service to respond...");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine($"Booking flight {origin} - {destination} on {travelDate:dd-MMM-yyyy}\n");
            return new DummyVacationPart($"Flight {origin} - {destination}");
        }
    }
}
