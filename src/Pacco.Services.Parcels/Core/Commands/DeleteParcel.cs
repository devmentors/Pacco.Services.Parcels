using System;
using Convey.CQRS.Commands;
using Convey.WebApi.CQRS;

namespace Pacco.Services.Parcels.Core.Commands
{
    [PublicContract]
    public class DeleteParcel : ICommand
    {
        public Guid Id { get; }

        public DeleteParcel(Guid id)
        {
            Id = id;
        }
    }
}