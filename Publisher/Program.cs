using Contracts;
using FastEndpoints;

var bld = WebApplication.CreateBuilder();
bld.WebHost.ConfigureKestrel(o => o.ListenLocalhost(5000));
var app = bld.Build();

app.MapRemote("http://localhost:6000", c =>
{
    c.RegisterEvent<SomethingHappened>();
});

app.MapGet("/event/{name}", async (string name) =>
{
    for (int i = 1; i <= 10; i++)
    {
        await new SomethingHappened
        {
            Id = i,
            Description = name
        }
        .RemotePublishAsync();

        await Task.Delay(500);
    }
    return Results.Ok("events published!");
});

app.Run();
