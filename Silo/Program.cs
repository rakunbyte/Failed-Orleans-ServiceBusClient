using GrainInterfaces;
using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());

        silo.Configure<GrainCollectionOptions>(options =>
        {
            // Set the value of CollectionAge to 10 minutes for all grain
            options.CollectionAge = TimeSpan.FromHours(24);

            // Override the value of CollectionAge to 5 minutes for MyGrainImplementation
            options.ClassSpecificCollectionAge[typeof(SubscriberGrain).FullName] =
                TimeSpan.FromHours(24);
        });
        
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