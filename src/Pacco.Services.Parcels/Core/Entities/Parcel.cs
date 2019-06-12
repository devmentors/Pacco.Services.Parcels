using System;

namespace Pacco.Services.Parcels.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Variant Variant { get; private set; }
        public Dimension Dimension { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}