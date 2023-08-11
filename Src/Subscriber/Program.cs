using Contracts;
using FastEndpoints;
using Subscriber;

var bld = WebApplication.CreateBuilder();
bld.WebHost.ConfigureKestrel(o => o.ListenLocalhost(7000));
var app = bld.Build();

app.MapRemote("http://localhost:6000", c =>
{
    c.Subscribe<SomethingHappened, WhenSomethingHappens>();
});

app.Run();

namespace Subscriber { public partial class Program { } };