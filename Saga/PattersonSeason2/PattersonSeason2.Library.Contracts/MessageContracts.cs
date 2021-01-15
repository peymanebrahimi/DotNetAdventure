using MassTransit;
using MassTransit.Topology.Topologies;

namespace PattersonSeason2.Library.Contracts
{
    public class MessageContracts
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            GlobalTopology.Send.UseCorrelationId<BookAdded>(x => x.BookId);
            GlobalTopology.Send.UseCorrelationId<ReservationRequested>(x => x.ReservationId);

            _initialized = true;
        }
    }
}