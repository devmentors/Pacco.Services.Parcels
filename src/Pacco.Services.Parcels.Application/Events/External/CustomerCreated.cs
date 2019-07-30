using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Parcels.Application.Events.External
{
    [MessageNamespace("customers")]
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; }

        public CustomerCreated(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}