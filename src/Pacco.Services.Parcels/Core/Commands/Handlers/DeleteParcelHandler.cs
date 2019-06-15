using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;

namespace Pacco.Services.Parcels.Core.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly ILogger<DeleteParcel> _logger;

        public DeleteParcelHandler(ILogger<DeleteParcel> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            _logger.LogInformation($"Deleting a parcel: {command.Id}");
            await Task.CompletedTask;
        }
    }
}