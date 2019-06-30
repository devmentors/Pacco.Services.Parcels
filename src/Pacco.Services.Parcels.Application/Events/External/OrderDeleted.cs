using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Parcels.Application.Events.External
{
    [MessageNamespace("orders")]
    public class OrderDeleted : IEvent
    {
        public Guid Id { get; }

        public OrderDeleted(Guid id)
        {
            Id = id;
        }
    }
}