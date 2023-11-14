using MassTransit;
using System;

namespace PattersonSeason2.Library.Components
{
    public class Reservation : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Reserved { get; set; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
    }
}