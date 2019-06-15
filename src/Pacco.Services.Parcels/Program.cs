using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Parcels.Core.Commands;
using Pacco.Services.Parcels.Core.DTO;
using Pacco.Services.Parcels.Core.Entities;
using Pacco.Services.Parcels.Core.Queries;
using Pacco.Services.Parcels.Core.Repositories;

namespace Pacco.Services.Parcels
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddCommandHandlers()
                    .AddEventHandlers()
                    .AddQueryHandlers()
                    .AddInMemoryCommandDispatcher()
                    .AddInMemoryEventDispatcher()
                    .AddInMemoryQueryDispatcher()
                    .AddWebApi()
                    .AddMongo()
                    .AddMongoRepository<Parcel, Guid>("Parcels"))
                .Configure(app => app
                    .UseErrorHandler()
                    .UsePublicContracts()
                    .UseEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Parcels Service!")))
                    .UseDispatcherEndpoints(endpoints =>
                    {
                        endpoints.Get<GetParcels, IEnumerable<ParcelDto>>("parcels");
                        endpoints.Post<AddParcel>("parcels",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"parcels/{cmd.Id}"));
                        endpoints.Delete("parcels/{id}",
                            ctx => ctx.SendAsync(new DeleteParcel(ctx.Args<Guid>("id"))));
                    }))
                .Build()
                .RunAsync();
    }
}
