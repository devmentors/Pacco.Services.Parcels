using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Parcels.Application.Events;
using Pacco.Services.Parcels.Application.Exceptions;
using Pacco.Services.Parcels.Application.Services;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Core.Exceptions;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<AddParcelHandler> _logger;

        public AddParcelHandler(IParcelRepository parcelRepository, ICustomerRepository customerRepository,
            IMessageBroker messageBroker, IDateTimeProvider dateTimeProvider, ILogger<AddParcelHandler> logger)
        {
            _parcelRepository = parcelRepository;
            _customerRepository = customerRepository;
            _messageBroker = messageBroker;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task HandleAsync(AddParcel command)
        {
            if (!Enum.TryParse<Variant>(command.Variant, true, out var variant))
            {
                _logger.LogWarning($"Invalid parcel variant: {command.Variant}.");
                throw new InvalidParcelVariantException(command.Variant);
            }

            if (!Enum.TryParse<Size>(command.Size, true, out var size))
            {
                _logger.LogWarning($"Invalid parcel size: {command.Size}.");
                throw new InvalidParcelSizeException(command.Size);
            }

            if (!(await _customerRepository.ExistsAsync(command.CustomerId)))
            {
                _logger.LogWarning($"Customer with id: {command.CustomerId} was not found.");
                throw new CustomerNotFoundException(command.CustomerId);
            }

            var parcel = new Parcel(command.ParcelId, command.CustomerId, variant, size, command.Name,
                command.Description, _dateTimeProvider.Now);
            await _parcelRepository.AddAsync(parcel);
            await _messageBroker.PublishAsync(new ParcelAdded(command.ParcelId));
            _logger.LogInformation($"Added a parcel with id: {command.ParcelId} the customer: {command.CustomerId}");
        }
    }
}

