using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Parcels.Application.Events
{
    public class DeleteParcelRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public DeleteParcelRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}