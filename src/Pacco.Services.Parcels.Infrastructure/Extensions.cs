using System;
using Convey;
using Convey.Persistence.MongoDB;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Parcels.Application;
using Pacco.Services.Parcels.Application.Commands;
using Pacco.Services.Parcels.Core.Repositories;
using Pacco.Services.Parcels.Infrastructure.MessageBrokers;
using Pacco.Services.Parcels.Infrastructure.Mongo.Documents;
using Pacco.Services.Parcels.Infrastructure.Mongo.Repositories;

namespace Pacco.Services.Parcels.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IParcelRepository, ParcelMongoRepository>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddRabbitMq()
                .AddMongo()
                .AddMongoRepository<ParcelDocument, Guid>("parcels");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UsePublicContracts(false)
                .UseInitializers()
                .UseRabbitMq()
                .SubscribeCommand<AddParcel>()
                .SubscribeCommand<DeleteParcel>();

            return app;
        }
    }
}