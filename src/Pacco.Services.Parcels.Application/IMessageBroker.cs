using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace Pacco.Services.Parcels.Application
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}