using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels.Application.Commands.Handlers
{
    public class AddParcelHandler : ICommandHandler<AddParcel>
    {
        private readonly IParcelRepository _parcelRepository;

        public AddParcelHandler(IParcelRepository parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }

        public async Task HandleAsync(AddParcel command)
        {
            await _parcelRepository.AddAsync(new Parcel(command.Id, command.CustomerId,
                Variant.Standard, Size.Normal, command.Name, command.Description));
        }
    }
}