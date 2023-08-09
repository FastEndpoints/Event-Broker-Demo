using Contracts;
using FastEndpoints;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Broker;

sealed class Program
{
    private static void Main()
    {
        var bld = WebApplication.CreateBuilder();
        bld.WebHost.ConfigureKestrel(o => o.ListenLocalhost(6000, o => o.Protocols = HttpProtocols.Http2));
        bld.AddHandlerServer();

        var app = bld.Build();
        app.MapHandlers(h =>
        {
            h.RegisterEventHub<SomethingHappened>(HubMode.EventBroker);
        });

        app.Run();
    }
}