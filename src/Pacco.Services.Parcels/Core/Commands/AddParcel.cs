using System;
using Convey.CQRS.Commands;
using Convey.WebApi.CQRS;

namespace Pacco.Services.Parcels.Core.Commands
{
    [PublicMessage]
    public class AddParcel : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Variant { get; }
        public string Dimension { get; }
        public string Name { get; }
        public string Description { get; }

        public AddParcel(Guid id, Guid customerId, string variant, string dimension, string name, string description)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            CustomerId = customerId;
            Variant = variant;
            Dimension = dimension;
            Name = name;
            Description = description;
        }
    }
}