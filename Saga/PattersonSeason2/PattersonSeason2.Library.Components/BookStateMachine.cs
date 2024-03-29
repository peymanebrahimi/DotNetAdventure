﻿using MassTransit;
using PattersonSeason2.Library.Contracts;

namespace PattersonSeason2.Library.Components
{
    public class BookStateMachine : MassTransitStateMachine<Book>
    {
        static BookStateMachine()
        {
            MessageContracts.Initialize();
        }

        public BookStateMachine()
        {

            InstanceState(x => x.CurrentState, Available, Reserved);

            Event(() => ReservationRequested,
                x =>
                    x.CorrelateById(m => m.Message.BookId));


            Initially(
                When(Added)
                    .CopyDataToInstance()
                    .TransitionTo(Available)
                );

            During(Available,
                When(ReservationRequested)
                    .TransitionTo(Reserved)
                    .PublishAsync(context => context.Init<BookReserved>(new
                    {
                        context.Data.ReservationId,
                        context.Data.MemberId,
                        context.Data.BookId,
                        InVar.Timestamp
                    })));
        }

        public Event<BookAdded> Added { get; }
        public Event<ReservationRequested> ReservationRequested { get; }
        public State Available { get; }
        public State Reserved { get; }
    }

    public static class BookStateMachineExtensions
    {
        public static EventActivityBinder<Book, BookAdded> CopyDataToInstance(
            this EventActivityBinder<Book, BookAdded> binder)
        {
            return binder.Then(x =>
            {
                x.Instance.DateAdded = x.Data.TimeStamp.Date;
                x.Instance.Title = x.Data.Title;
                x.Instance.Isbn = x.Data.Isbn;
            });
        }
    }
}