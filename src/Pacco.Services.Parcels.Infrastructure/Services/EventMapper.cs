using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Application.Services;
using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case Core.Events.ParcelAdded e:
                    return new ParcelAdded(e.Parcel.Id);
            }

            return null;
        }
    }
}