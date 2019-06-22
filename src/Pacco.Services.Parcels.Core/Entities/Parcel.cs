using System;
using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public AggregateId Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Variant Variant { get; private set; }
        public Size Size { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Parcel(AggregateId id, Guid customerId, Variant variant, Size size, string name, string description)
        {
            Id = id;
            CustomerId = customerId;
            Variant = variant;
            Size = size;
            Name = string.IsNullOrWhiteSpace(name) ? throw new InvalidParcelNameException(name) : name;
            Description = string.IsNullOrWhiteSpace(description)
                ? throw new InvalidParcelDescriptionException(description)
                : description;
        }
    }
}