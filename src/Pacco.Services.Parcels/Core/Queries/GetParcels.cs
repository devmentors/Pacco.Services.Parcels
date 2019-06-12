using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using Pacco.Services.Parcels.Core.DTO;

namespace Pacco.Services.Parcels.Core.Queries
{
    public class GetParcels : IQuery<IEnumerable<ParcelDto>>
    {
        public Guid CustomerId { get; set; }
    }
}