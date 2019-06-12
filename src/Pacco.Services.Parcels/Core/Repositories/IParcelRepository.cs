using System;
using System.Threading.Tasks;
using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Core.Repositories
{
    public interface IParcelRepository
    {
        Task AddAsync(Parcel parcel);
        Task<Parcel> BrowseAsync(Guid customerId);
        Task DeleteAsync(Guid id);
    }
}