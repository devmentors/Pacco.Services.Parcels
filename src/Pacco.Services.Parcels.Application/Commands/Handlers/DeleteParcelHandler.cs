using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class DeleteParcelHandler : ICommandHandler<DeleteParcel>
    {
        private readonly IParcelRepository _parcelRepository;

        public DeleteParcelHandler(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }

        public async Task HandleAsync(DeleteParcel command)
        {
            await _parcelRepository.DeleteAsync(command.Id);
        }
    }
}