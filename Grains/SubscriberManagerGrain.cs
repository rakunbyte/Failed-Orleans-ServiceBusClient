using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class SubscriberManagerGrain: Grain, ISubscriberManagerGrain
{
    private readonly ILogger _logger;

    public SubscriberManagerGrain(ILogger<SubscriberManagerGrain> logger) => _logger = logger;
    
    public Task DoWork()
    {
        _logger.LogInformation($"Subscription Manager Work Started: {IdentityString}");

        var partitions = 3;

        for (var i = 0; i < partitions; i++)
        {
            _ = GrainFactory.GetGrain<ISubscriberGrain>(i).DoWork();
        }

        return Task.CompletedTask;
    }
}