using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Pacco.Services.Parcels.Core.DTO;

namespace Pacco.Services.Parcels.Core.Queries.Handlers
{
    public class GetParcelsHandler : IQueryHandler<GetParcels, IEnumerable<ParcelDto>>
    {
        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcels query)
        {
            await Task.CompletedTask;
            return new List<ParcelDto>();
        }
    }
}