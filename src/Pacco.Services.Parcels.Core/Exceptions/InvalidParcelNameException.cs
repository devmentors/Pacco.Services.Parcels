namespace Pacco.Services.Parcels.Core.Exceptions
{
    public class InvalidParcelNameException : ExceptionBase
    {
        public override string Code => "invalid_parcel_name";

        public InvalidParcelNameException(string name) : base($"Parcel name is invalid: {name}.")
        {
        }
    }
}