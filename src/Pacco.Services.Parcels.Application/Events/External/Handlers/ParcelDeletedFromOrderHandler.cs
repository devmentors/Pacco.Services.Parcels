using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Events.External.Handlers
{
    public class ParcelDeletedFromOrderHandler : IEventHandler<ParcelDeletedFromOrder>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ILogger<ParcelDeletedFromOrderHandler> _logger;

        public ParcelDeletedFromOrderHandler(IParcelRepository parcelRepository,
            ILogger<ParcelDeletedFromOrderHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ParcelDeletedFromOrder @event)
        {
            var parcel = await _parcelRepository.GetAsync(@event.ParcelId);
            if (parcel is null)
            {
                return;
            }

            parcel.DeleteFromOrder();
            await _parcelRepository.UpdateAsync(parcel);
            _logger.LogInformation($"Parcel with id: {@event.ParcelId} was deleted from the order " +
                                   $"with id: {@event.OrderId}");
        }
    }
}