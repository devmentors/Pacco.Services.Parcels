using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Parcels.Application.Services;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddParcelHandler(IParcelRepository parcelRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker, IDateTimeProvider dateTimeProvider)
        {
            _parcelRepository = parcelRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(AddParcel command)
        {
            var parcel = new Parcel(command.Id, command.CustomerId, Variant.Standard,
                Size.Normal, command.Name, command.Description, _dateTimeProvider.Now);
            await _parcelRepository.AddAsync(parcel);
            var events = _eventMapper.MapAll(parcel.Events).ToArray();
            await _messageBroker.PublishAsync(events);
        }
    }
}

