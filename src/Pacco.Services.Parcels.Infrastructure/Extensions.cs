using System;
using Convey;
using Convey.Persistence.MongoDB;
using Convey.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Parcels.Core.Repositories;
using Pacco.Services.Parcels.Infrastructure.Mongo.Documents;
using Pacco.Services.Parcels.Infrastructure.Mongo.Repositories;

namespace Pacco.Services.Parcels.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IParcelRepository, ParcelMongoRepository>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddMongo()
                .AddMongoRepository<ParcelDocument, Guid>("Parcels");
        }
    }
}