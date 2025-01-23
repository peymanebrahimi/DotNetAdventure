namespace Reservations
{
    public class HotelInfo
    {
        public string Town { get; set; }
        public string HotelName { get; set; }
        public override string ToString()
        {
            return $"{this.HotelName} in {this.Town}";
        }
    }
}
