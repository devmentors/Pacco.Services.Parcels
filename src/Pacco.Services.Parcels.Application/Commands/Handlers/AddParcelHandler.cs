using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AddParcelHandler> _logger;

        public AddParcelHandler(IParcelRepository parcelRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker, IDateTimeProvider dateTimeProvider,
            ILogger<AddParcelHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task HandleAsync(AddParcel command)
        {
            var parcel = new Parcel(command.Id, command.CustomerId, Variant.Standard,
                Size.Normal, command.Name, command.Description, _dateTimeProvider.Now);
            await _parcelRepository.AddAsync(parcel);
            _logger.LogInformation($"Added a parcel with id: {command.Id} for customer: {command.CustomerId}");
            var events = _eventMapper.MapAll(parcel.Events).ToArray();
            await _messageBroker.PublishAsync(events);
        }
    }
}

