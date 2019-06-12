using System;
using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Core.DTO
{
    public class ParcelDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Variant { get; set; }
        public string Dimension { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ParcelDto()
        {
        }

        public ParcelDto(Parcel parcel)
        {
            Id = parcel.Id;
            CustomerId = parcel.CustomerId;
            Variant = parcel.Variant.ToString();
            Dimension = parcel.Dimension.ToString();
            Name = parcel.Name;
            Description = parcel.Description;
        }
    }
}