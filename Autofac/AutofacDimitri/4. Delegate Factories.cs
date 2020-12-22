using System;

namespace AutofacDimitri
{
    class Test
    {
        private Shareholding.Factory1 f1 = new Shareholding.Factory1(Factory1);

        private static Shareholding Factory1(string symbol, uint holding)
        {
            throw new NotImplementedException();
        }

        public void DoSomething()
        {
            var shareholding = f1("a", 2);

            var f2 = new Shareholding("s", 2);
            f2.Factory2 = (s, u) => new Shareholding(s, u);
        }
    }

    public class Shareholding
    {
        public delegate Shareholding Factory1(string symbol, uint holding);

        public Func<string, uint, Shareholding> Factory2 { get; set; }

        public Shareholding(string symbol, uint holding)
        {
            Symbol = symbol;
            Holding = holding;
        }

        public string Symbol { get; private set; }

        public uint Holding { get; set; }
    }


}
