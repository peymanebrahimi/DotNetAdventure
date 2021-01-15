using System;

namespace PattersonSeason2.Library.Contracts
{
    public interface BookAdded
    {
        Guid BookId { get; }
        DateTime TimeStamp { get; }
        string Isbn { get; }
        string Title { get; }
    }
}