using System;
using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Application.Exceptions
{
    public class CustomerAlreadyExistsException : ExceptionBase
    {
        public override string Code => "customer_already_exists";
        public Guid CustomerId { get; }

        public CustomerAlreadyExistsException(Guid customerId) 
            : base($"Customer with id: {customerId} already exists.")
        {
            CustomerId = customerId;
        }
    }
}