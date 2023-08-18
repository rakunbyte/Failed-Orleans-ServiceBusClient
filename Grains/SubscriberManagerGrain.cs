using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class SubscriberManagerGrain: Grain, ISubscriberManagerGrain
{
    private readonly ILogger _logger;

    public SubscriberManagerGrain(ILogger<SubscriberManagerGrain> logger) => _logger = logger;
    
    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        _logger.LogError("SUBSCRIMBER Manager GRAIN HAS SHUT DOWN!!!!!! " + reason);
        return base.OnDeactivateAsync(reason, cancellationToken);
    }
    
    public Task DoWork(string topic, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Subscription Manager Work Started: {IdentityString}");

        

        return Task.CompletedTask;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var partitions = 3;

        //Need to manage these lifecycles somehow
        for (var i = 0; i < partitions; i++)
        {
            _ = GrainFactory.GetGrain<ISubscriberGrain>(i).DoWork("topic1", cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }
}