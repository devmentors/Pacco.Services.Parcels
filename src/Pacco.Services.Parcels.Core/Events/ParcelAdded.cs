using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Core.Events
{
    public class ParcelAdded : IDomainEvent
    {
        public Parcel Parcel { get; }

        public ParcelAdded(Parcel parcel)
        {
            Parcel = parcel;
        }
    }
}