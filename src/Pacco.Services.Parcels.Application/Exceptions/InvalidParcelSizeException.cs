using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Application.Exceptions
{
    public class InvalidParcelSizeException : ExceptionBase
    {
        public override string Code => "invalid_parcel_size";
        public string Size { get; }

        public InvalidParcelSizeException(string size) : base($"Invalid parcel size: {size}")
        {
            Size = size;
        }
    }
}