using Contracts;
using FastEndpoints;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var bld = WebApplication.CreateBuilder();
bld.WebHost.ConfigureKestrel(o => o.ListenLocalhost(6000, o => o.Protocols = HttpProtocols.Http2));
bld.AddHandlerServer();

var app = bld.Build();
app.MapHandlers(h =>
{
    h.RegisterEventHub<SomethingHappened>(HubMode.EventBroker);
});
app.Run();

namespace Broker { public partial class Program { } };
