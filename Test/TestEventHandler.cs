using Contracts;
using FastEndpoints;

namespace Test;

sealed class TestEventHandler : IEventHandler<SomethingHappened>
{
    internal static List<SomethingHappened> _received = new();

    public Task HandleAsync(SomethingHappened e, CancellationToken ct)
    {
        _received.Add(e);
        return Task.CompletedTask;
    }

    public static async Task<bool> IsTestPassed()
    {
        while (_received.Count < 10)
        {
            await Task.Delay(100);
        }

        var ids = !_received.Select(x => x.Id).Except(Enumerable.Range(1, 10)).Any();
        var desc = _received.All(e => e.Description == "AAAA");

        return ids && desc;
    }
}
