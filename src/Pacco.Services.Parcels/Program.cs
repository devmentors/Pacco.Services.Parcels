using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Parcels.Core.Commands;
using Pacco.Services.Parcels.Core.DTO;
using Pacco.Services.Parcels.Core.Queries;

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
                    .AddWebApi())
                .Configure(app => app
                    .UseErrorHandler()
                    .UsePublicMessages()
                    .UseEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Parcels Service!")))
                    .UseDispatcherEndpoints(endpoints =>
                    {
                        endpoints.Get<GetParcels, IEnumerable<ParcelDto>>("parcels");
                        endpoints.Post<AddParcel>("parcels",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"parcels/{cmd.Id}"));
                    }))
                .Build()
                .RunAsync();
    }
}
