using System;
using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Application.Exceptions
{
    public class UnauthorizedParcelAccessException : ExceptionBase
    {
        public override string Code => "unauthorized_parcel_access";

        public UnauthorizedParcelAccessException(Guid id, Guid customerId) 
            : base($"Unauthorized access to parcel: '{id}' by customer: '{customerId}'")
        {
        }
    }
}