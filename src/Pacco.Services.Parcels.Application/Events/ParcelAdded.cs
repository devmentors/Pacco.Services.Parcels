using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Parcels.Application.Events
{
    [Contract]
    public class ParcelAdded : IEvent
    {
        public Guid Id { get; }

        public ParcelAdded(Guid id)
        {
            Id = id;
        }
    }
}