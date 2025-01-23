namespace Reservations
{
    public class DummyVacationPart(string name) : IVacationPart
    {
        public void Reserve()
        {
            Console.WriteLine($"Making reservation - {name}");
        }

        public void Purchase()
        {
            Console.WriteLine($"Purchasing {name}");
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}
