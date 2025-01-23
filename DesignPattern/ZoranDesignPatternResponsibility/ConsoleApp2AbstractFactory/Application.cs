namespace Reservations
{
    public class Application(IVacationPartFactory partFactory)
    {
        public void Run()
        {

            VacationSpecificationBuilder builder = 
                new VacationSpecificationBuilder(
                    partFactory,
                    new DateTime(2015, 7, 11), 14,
                    "Crouded City", "Seatown");

            builder.SelectHotel("Small one");
            builder.SelectAirplane("Noisy one");

            VacationSpecification spec = builder.BuildVacation();

        }
    }
}
