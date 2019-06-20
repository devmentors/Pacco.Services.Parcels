using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IMessageBroker _messageBroker;

        public AddParcelHandler(IParcelRepository parcelRepository, IMessageBroker messageBroker)
        {
            _parcelRepository = parcelRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(AddParcel command)
        {
            await _parcelRepository.AddAsync(new Parcel(command.Id, command.CustomerId,
                Variant.Standard, Size.Normal, command.Name, command.Description));
            await _messageBroker.PublishAsync(new ParcelAdded(command.Id));
        }
    }
}