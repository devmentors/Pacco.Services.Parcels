using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pacco.Services.Parcels.Application.DTO;
using Pacco.Services.Parcels.Application.Queries;
using Pacco.Services.Parcels.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Parcels.Infrastructure.Mongo.Queries.Handlers
{
    public class GetParcelsHandler : IQueryHandler<GetParcels, IEnumerable<ParcelDto>>
    {
        private readonly IMongoRepository<ParcelDocument, Guid> _parcelRepository;

        public GetParcelsHandler(IMongoRepository<ParcelDocument, Guid> parcelRepository)
        {
            _parcelRepository = parcelRepository;
        }

        public async Task<IEnumerable<ParcelDto>> HandleAsync(GetParcels query)
        {
            var documents = _parcelRepository.Collection.AsQueryable();
            if (query.CustomerId.HasValue)
            {
                documents = documents.Where(p => p.CustomerId == query.CustomerId);
            }

            if (!query.IncludeAddedToOrders)
            {
                documents = documents.Where(p => !p.AddedToOrder);
            }

            var parcels = await documents.ToListAsync();

            return parcels.Select(p => p.AsDto());
        }
    }
}