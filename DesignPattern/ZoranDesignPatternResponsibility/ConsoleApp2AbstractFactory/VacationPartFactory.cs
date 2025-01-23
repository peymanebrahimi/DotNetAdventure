namespace Reservations
{
    public class VacationPartFactory(
        IHotelSelector hotelSelector,
        IHotelService hotelService,
        IAirplaneService airplaneService)
        : IVacationPartFactory
    {
        public IVacationPart CreateHotelReservation(string town, string hotelName,
                                                    DateTime arrivalDate,
                                                    DateTime leaveDate)
        {
            HotelInfo hotel = hotelSelector.SelectHotel(town, hotelName);
            return hotelService.MakeBooking(hotel, arrivalDate, leaveDate);
        }

        public IVacationPart CreateFlight(string companyName, string source,
                                          string destination, DateTime date)
        {
            return
                airplaneService
                .SelectFlight(companyName, source, destination, date);
        }

        public IVacationPart CreateFerryBooking(string lineName, bool fromMainland,
                                                DateTime date)
        {
            throw new NotImplementedException();
        }

        public IVacationPart CreateDailyTrip(string tripName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IVacationPart CreateMassage(string salon, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
