using Pacco.Services.Parcels.Core.Exceptions;

namespace Pacco.Services.Parcels.Application.Exceptions
{
    public class InvalidParcelVariantException : ExceptionBase
    {
        public override string Code => "invalid_parcel_variant";
        public string Variant { get; }

        public InvalidParcelVariantException(string variant) : base($"Invalid parcel variant: {variant}")
        {
            Variant = variant;
        }
    }
}