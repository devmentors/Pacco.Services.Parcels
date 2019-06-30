using System;
using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Parcels.Application.Commands;
using Pacco.Services.Parcels.Application.Events.Rejected;
using Pacco.Services.Parcels.Application.Exceptions;
using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
            if (message is DeleteParcel)
            {

            }

            switch (exception)
            {
                case CannotDeleteParcelException ex: return new DeleteParcelRejected(ex.Id, ex.Message, ex.Code);
                case InvalidParcelVariantException ex: return new AddParcelRejected(ex.Message, ex.Code);
                case InvalidParcelSizeException ex: return new AddParcelRejected(ex.Message, ex.Code);
                case InvalidParcelNameException ex: return new AddParcelRejected(ex.Message, ex.Code);
                case InvalidParcelDescriptionException ex: return new AddParcelRejected(ex.Message, ex.Code);
                case ParcelNotFoundException ex:
                    return message is DeleteParcel ? new DeleteParcelRejected(Guid.Empty, ex.Message, ex.Code) : null;
            }

            return null;
        }
    }
}