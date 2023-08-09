using Contracts;
using FastEndpoints;

namespace Subscriber;

sealed class Program
{
    private static void Main()
    {
        var bld = WebApplication.CreateBuilder();
        bld.WebHost.ConfigureKestrel(o => o.ListenLocalhost(7000));
        var app = bld.Build();

        app.MapRemote("http://localhost:6000", c =>
        {
            c.Subscribe<SomethingHappened, WhenSomethingHappens>();
        });

        app.Run();
    }
}