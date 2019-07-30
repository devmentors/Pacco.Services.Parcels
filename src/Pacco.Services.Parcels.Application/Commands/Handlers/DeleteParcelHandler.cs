using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Application.Exceptions;
using Pacco.Services.Parcels.Application.Services;
using Pacco.Services.Parcels.Core.Exceptions;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<DeleteParcelHandler> _logger;

        public DeleteParcelHandler(IParcelRepository parcelRepository, IMessageBroker messageBroker,
            ILogger<DeleteParcelHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            var parcel = await _parcelRepository.GetAsync(command.ParcelId);
            if (parcel is null)
            {
                throw new ParcelNotFoundException(command.ParcelId);
            }

            if (command.CustomerId.HasValue && command.CustomerId != parcel.CustomerId)
            {
                throw new UnauthorizedParcelAccessException(command.ParcelId, command.CustomerId.Value);
            }

            if (parcel.AddedToOrder)
            {
                throw new CannotDeleteParcelException(command.ParcelId);
            }

            await _parcelRepository.DeleteAsync(command.ParcelId);
            _logger.LogInformation($"Deleted a parcel with id: {command.ParcelId}");
            await _messageBroker.PublishAsync(new ParcelDeleted(command.ParcelId));
        }
    }
}