using System;

namespace Pacco.Services.Parcels.Core.Exceptions
{
    public class CannotDeleteParcelException : ExceptionBase
    {
        public override string Code => "cannot_delete_parcel";
        public Guid Id { get; }
        
        public CannotDeleteParcelException(Guid id) : base($"Parcel with id: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}