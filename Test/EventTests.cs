namespace Test;

public class EventTests : IClassFixture<PublisherFixture>
{
    public readonly HttpClient _publisherClient;

    public EventTests(PublisherFixture fixture)
    {
        _publisherClient = fixture.PublisherClient;
    }

    [Fact]
    public async Task Published_Events_Are_Received_By_Subscriber()
    {
        var res = await _publisherClient.GetStringAsync("/event/AAAA");

        Assert.Equal("\"events published!\"", res);
        Assert.True(await TestEventHandler.IsTestPassed());
    }
}