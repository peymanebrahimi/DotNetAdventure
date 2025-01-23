namespace Reservations
{
    public class VacationSpecificationBuilder(
        IVacationPartFactory partFactory,
        DateTime arrivalDate,
        int totalNights,
        string livingTown,
        string destinationTown)
    {

        private readonly IList<IVacationPart> _parts = new List<IVacationPart>();

        public void SelectHotel(string hotelName)
        {
            IVacationPart part = 
                partFactory
                .CreateHotelReservation(destinationTown, hotelName,
                                        arrivalDate,
                                        arrivalDate.AddDays(totalNights));
            this._parts.Add(part);
        }

        public void SelectAirplane(string companyName)
        {
            this._parts.Add(CreateFlightToDestination(companyName));
            this._parts.Add(CreateFlightBack(companyName));
        }

        private IVacationPart CreateFlightToDestination(string companyName)
        {
            return
                partFactory
                .CreateFlight(companyName, livingTown, destinationTown,
                              arrivalDate);
        }

        private IVacationPart CreateFlightBack(string companyName)
        {
            return
                partFactory
                .CreateFlight(companyName, destinationTown, livingTown,
                              arrivalDate.AddDays(totalNights));
        }

        private DateTime DepartureDate => arrivalDate.AddDays(totalNights);

        public VacationSpecification BuildVacation()
        {
            foreach (IVacationPart part in _parts)
                part.Reserve();
            return new VacationSpecification(_parts);
        }

    }
}
