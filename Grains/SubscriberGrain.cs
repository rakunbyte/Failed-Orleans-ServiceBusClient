using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class SubscriberGrain : Grain, ISubscriberGrain
{
    private readonly ILogger _logger;

    public SubscriberGrain(ILogger<SubscriberGrain> logger) => _logger = logger;
 
    public Task DoWork()
    {
        _logger.LogInformation($"Subscription Work Started: {IdentityString}");

        return Task.CompletedTask;
    }
}