using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Events.External.Handlers
{
    public class OrderDeletedHandler : IEventHandler<OrderDeleted>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ILogger<OrderDeletedHandler> _logger;

        public OrderDeletedHandler(IParcelRepository parcelRepository, ILogger<OrderDeletedHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _logger = logger;
        }

        public async Task HandleAsync(OrderDeleted @event)
        {
            var parcel = await _parcelRepository.GetByOrderAsync(@event.OrderId);
            if (parcel is null)
            {
                return;
            }

            parcel.DeleteFromOrder();
            await _parcelRepository.UpdateAsync(parcel);
            _logger.LogInformation($"Order with id: {@event.OrderId} was deleted." +
                                   $"Parcel with id: {parcel.Id} can be added to the new order again.");
        }
    }
}