using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class DirectorGrain : Grain, IDirectorGrain
{
    private readonly ILogger _logger;

    public DirectorGrain(ILogger<DirectorGrain> logger) => _logger = logger;

    public Task DoWork()
    {
        _logger.LogInformation("Director Work Started");

        return Task.CompletedTask;
    }
    
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        //Start Subscription Managers
        _ = GrainFactory.GetGrain<ISubscriberManagerGrain>(0).DoWork();
        _ = GrainFactory.GetGrain<ISubscriberManagerGrain>(1).DoWork();

        await base.OnActivateAsync(cancellationToken);
    }
}