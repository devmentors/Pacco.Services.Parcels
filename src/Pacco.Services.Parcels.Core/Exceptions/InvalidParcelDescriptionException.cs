namespace Pacco.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelDescriptionException : ExceptionBase
    {
        public override string Code => "invalid_parcel_description";

        public InvalidParcelDescriptionException(string description)
            : base($"Parcel description is invalid: {description}.")
        {
        }
    }
}