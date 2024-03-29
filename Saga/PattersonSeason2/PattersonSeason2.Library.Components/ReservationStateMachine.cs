﻿using MassTransit;
using PattersonSeason2.Library.Contracts;

namespace PattersonSeason2.Library.Components
{
    public class ReservationStateMachine : MassTransitStateMachine<Reservation>
    {
        static ReservationStateMachine()
        {
            MessageContracts.Initialize();
        }

        public ReservationStateMachine()
        {
            InstanceState(x => x.CurrentState, Requested);

            Event(() => BookReserved,
                x =>
                    x.CorrelateById(m => m.Message.ReservationId));

            Initially(
                When(ReservationRequested)
                    .Then(context =>
                    {
                        context.Instance.BookId = context.Data.BookId;
                        context.Instance.MemberId = context.Data.MemberId;
                        context.Instance.Created = context.Data.TimeStamp;
                    })
                    .TransitionTo(Requested)
            );

            During(Requested,
                When(BookReserved)
                    .Then(context => context.Instance.Reserved = context.Data.TimeStamp)
                    .TransitionTo(Reserved)
            );
        }

        public State Requested { get; }
        public State Reserved { get; }

        public Event<BookReserved> BookReserved { get; }
        public Event<ReservationRequested> ReservationRequested { get; }
    }
}