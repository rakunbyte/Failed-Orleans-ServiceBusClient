using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
        
        silo.AddStartupTask(
            async (IServiceProvider services, CancellationToken cancellation) =>
            {
                var grainFactory = services.GetRequiredService<IGrainFactory>();
                
                var directorGrain = grainFactory.GetGrain<IDirectorGrain>(0);
                await directorGrain.DoWork();
            });
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();

await host.RunAsync();