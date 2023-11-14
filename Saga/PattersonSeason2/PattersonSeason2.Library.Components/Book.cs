using MassTransit;
using System;

namespace PattersonSeason2.Library.Components
{
    public class Book : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public DateTime DateAdded { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
    }
}
