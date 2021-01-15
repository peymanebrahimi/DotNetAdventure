using System;

namespace PattersonSeason2.Library.Contracts
{
    public interface BookReserved
    {
        Guid ReservationId { get; }
        DateTime TimeStamp { get; }
        Guid MemberId { get; }
        Guid BookId { get; }
    }
}
