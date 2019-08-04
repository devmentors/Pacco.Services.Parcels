using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Events.External.Handlers
{
    public class OrderCanceledHandler : IEventHandler<OrderCanceled>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ILogger<OrderCanceledHandler> _logger;

        public OrderCanceledHandler(IParcelRepository parcelRepository, ILogger<OrderCanceledHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _logger = logger;
        }

        public async Task HandleAsync(OrderCanceled @event)
        {
            var parcel = await _parcelRepository.GetByOrderAsync(@event.OrderId);
            if (parcel is null)
            {
                return;
            }

            parcel.DeleteFromOrder();
            await _parcelRepository.UpdateAsync(parcel);
            _logger.LogInformation($"Order with id: {@event.OrderId} was canceled." +
                                   $"Parcel with id: {parcel.Id} can be added to the new order again.");
        }
    }
}