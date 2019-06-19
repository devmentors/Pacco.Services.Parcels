using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Parcels.Application.Commands
{
    public class DeleteParcel : ICommand
    {
        public Guid Id { get; }

        public DeleteParcel(Guid id)
        {
            Id = id;
        }
    }
}