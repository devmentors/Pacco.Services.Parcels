using Convey.CQRS.Events;

namespace Pacco.Services.Parcels.Application.Events
{
    [Contract]
    public class AddParcelRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        public AddParcelRejected(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }
}