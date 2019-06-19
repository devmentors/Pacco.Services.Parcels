using System.Threading.Tasks;
using Pacco.Services.Parcels.Core.Entities;

namespace Pacco.Services.Parcels.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        internal static Parcel AsEntity(this ParcelDocument document)
            => new Parcel(document.Id, document.CustomerId, document.Variant, document.Size, document.Name,
                document.Description);

        internal static async Task<Parcel> AsEntityAsync(this Task<ParcelDocument> task)
            => (await task).AsEntity();

        internal static ParcelDocument AsDocument(this Parcel entity)
            => new ParcelDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Variant = entity.Variant,
                Size = entity.Size,
                Name = entity.Name,
                Description = entity.Description
            };

        internal static async Task<ParcelDocument> AsDocumentAsync(this Task<Parcel> task)
            => (await task).AsDocument();
    }
}