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
        private readonly IAppContext _appContext;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<DeleteParcelHandler> _logger;

        public DeleteParcelHandler(IParcelRepository parcelRepository, IAppContext appContext,
            IMessageBroker messageBroker, ILogger<DeleteParcelHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _appContext = appContext;
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

            var identity = _appContext.Identity;
            if (identity.IsAuthenticated && identity.Id != parcel.CustomerId && !identity.IsAdmin)
            {
                _logger.LogWarning($"Customer with id: {identity.Id} tried to delete a parcel " +
                                   $"with id: {command.ParcelId} without the proper access rights.");
                throw new UnauthorizedParcelAccessException(parcel.Id, identity.Id);
            }

            if (parcel.AddedToOrder)
            {
                _logger.LogWarning($"Parcel with id: {command.ParcelId} belong to an order " +
                                   $"with id: {parcel.OrderId} and cannot be deleted.");
                throw new CannotDeleteParcelException(command.ParcelId);
            }

            await _parcelRepository.DeleteAsync(command.ParcelId);
            await _messageBroker.PublishAsync(new ParcelDeleted(command.ParcelId));
            _logger.LogInformation($"Deleted a parcel with id: {command.ParcelId}");
        }
    }
}