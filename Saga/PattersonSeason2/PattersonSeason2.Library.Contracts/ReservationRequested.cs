using System;

namespace PattersonSeason2.Library.Contracts
{
    public interface ReservationRequested
    {
        Guid ReservationId { get; set; }
        DateTime TimeStamp { get; }
        Guid MemberId { get; }
        Guid BookId { get; }
    }
}