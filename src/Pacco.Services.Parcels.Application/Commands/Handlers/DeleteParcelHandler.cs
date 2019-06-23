using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Application.Services;
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
            await _parcelRepository.DeleteAsync(command.Id);
            _logger.LogInformation($"Deleted a parcel with id: {command.Id}");
            await _messageBroker.PublishAsync(new ParcelDeleted(command.Id));
        }
    }
}