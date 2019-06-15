using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;

namespace Pacco.Services.Parcels.Core.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly ILogger<AddParcelHandler> _logger;

        public AddParcelHandler(ILogger<AddParcelHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(AddParcel command)
        {
            _logger.LogInformation($"Adding a parcel: {command.Id} {command.Name}");
            await Task.CompletedTask;
        }
    }
}