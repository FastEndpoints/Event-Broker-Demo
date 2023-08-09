using Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Test;

public class PublisherFixture : IDisposable
{
    public HttpClient PublisherClient { get; set; }

    private readonly WebApplicationFactory<Broker.Program> _broker = new();
    private readonly WebApplicationFactory<Subscriber.Program> _subscriber = new();
    private readonly WebApplicationFactory<Publisher.Program> _publisher = new();

    public PublisherFixture()
    {
        PublisherClient = _publisher.WithWebHostBuilder(c =>
        {
            c.ConfigureTestServices(s =>
            {
                s.RegisterTestRemote(_broker.Server); //connect publisher to broker
            });
        }).CreateClient();

        _ = _subscriber.WithWebHostBuilder(c =>
        {
            c.ConfigureTestServices(s =>
            {
                s.RegisterTestRemote(_broker.Server); //connect subscriber to broker
                s.RegisterTestEventHandler<SomethingHappened, TestEventHandler>(); //using a fake handler to assert receipt of events
            });
        }).CreateDefaultClient();
    }

    #region disposable
    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _publisher.Dispose();
                PublisherClient.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}