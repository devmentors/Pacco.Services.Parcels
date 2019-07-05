using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Parcels.Application.Commands
{
    [Contract]
    public class DeleteParcel : ICommand
    {
        public Guid Id { get; }
        public Guid? CustomerId { get; }

        public DeleteParcel(Guid id, Guid? customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}