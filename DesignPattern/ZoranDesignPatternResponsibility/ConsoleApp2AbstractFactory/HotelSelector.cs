namespace Reservations
{
    public class HotelSelector: IHotelSelector
    {
        public HotelInfo SelectHotel(string town, string hotelName)
        {
            Console.WriteLine($"Looking up hotel {hotelName} in {town}");
            return new HotelInfo() { Town = town, HotelName = hotelName };
        }
    }
}
