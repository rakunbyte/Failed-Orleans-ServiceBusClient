using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

using IHost host = builder.Build();
await host.StartAsync();

IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

//IHelloGrain friend = client.GetGrain<IHelloGrain>(0);
//string response = await friend.SayHello("Hi friend!");
var response = "dud";

Console.WriteLine($"""
                   {response}

                   Press any key to exit...
                   """);

Console.ReadKey();

await host.StopAsync();