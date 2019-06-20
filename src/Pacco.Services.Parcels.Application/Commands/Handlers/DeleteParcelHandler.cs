using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IMessageBroker _messageBroker;

        public DeleteParcelHandler(IParcelRepository parcelRepository, IMessageBroker messageBroker)
        {
            _parcelRepository = parcelRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            await _parcelRepository.DeleteAsync(command.Id);
            await _messageBroker.PublishAsync(new ParcelDeleted(command.Id));
        }
    }
}