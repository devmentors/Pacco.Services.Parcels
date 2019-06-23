using System.Collections.Generic;
using Convey.CQRS.Events;
using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}