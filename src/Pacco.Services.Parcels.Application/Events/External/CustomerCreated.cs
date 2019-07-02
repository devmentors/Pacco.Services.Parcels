using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Parcels.Application.Events.External
{
    [MessageNamespace("customers")]
    public class CustomerCreated : IEvent
    {
        public Guid Id { get; }

        public CustomerCreated(Guid id)
        {
            Id = id;
        }
    }
}