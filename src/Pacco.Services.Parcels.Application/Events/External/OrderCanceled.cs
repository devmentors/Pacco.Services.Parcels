using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Parcels.Application.Events.External
{
    [MessageNamespace("orders")]
    public class OrderCanceled : IEvent
    {
        public Guid Id { get; }
        public string Reason { get; }

        public OrderCanceled(Guid id, string reason)
        {
            Id = id;
            Reason = reason;
        }
    }
}