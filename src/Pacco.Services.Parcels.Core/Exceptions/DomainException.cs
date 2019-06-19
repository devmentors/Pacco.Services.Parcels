using System;

namespace Pacco.Services.Parcels.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}