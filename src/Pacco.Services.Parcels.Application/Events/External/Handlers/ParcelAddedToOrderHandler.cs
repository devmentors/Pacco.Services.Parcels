using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Events.External.Handlers
{
    public class ParcelAddedToOrderHandler : IEventHandler<ParcelAddedToOrder>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ILogger<ParcelAddedToOrderHandler> _logger;

        public ParcelAddedToOrderHandler(IParcelRepository parcelRepository, ILogger<ParcelAddedToOrderHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ParcelAddedToOrder @event)
        {
            var parcel = await _parcelRepository.GetAsync(@event.ParcelId);
            if (parcel is null)
            {
                return;
            }

            parcel.AddToOrder(@event.OrderId);
            await _parcelRepository.UpdateAsync(parcel);
            _logger.LogInformation($"Parcel with id: {@event.ParcelId} was added to the order " +
                                   $"with id: {@event.OrderId}");
        }
    }
}