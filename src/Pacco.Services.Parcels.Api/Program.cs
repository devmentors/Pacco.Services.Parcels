using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Parcels.Application;
using Pacco.Services.Parcels.Application.Commands;
using Pacco.Services.Parcels.Application.DTO;
using Pacco.Services.Parcels.Application.Queries;
using Pacco.Services.Parcels.Infrastructure;

namespace Pacco.Services.Parcels.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure())
                .Configure(app => app
                    .UseErrorHandler()
                    .UseInitializers()
                    .UsePublicContracts(false)
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Parcels Service!"))
                        .Get<GetParcels, IEnumerable<ParcelDto>>("parcels")
                        .Post<AddParcel>("parcels",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"parcels/{cmd.Id}"))
                        .Delete("parcels/{id}",
                            ctx => ctx.SendAsync(new DeleteParcel(ctx.Args<Guid>("id"))))
                    ).InitAsync())
                .Build()
                .RunAsync();
    }
}
