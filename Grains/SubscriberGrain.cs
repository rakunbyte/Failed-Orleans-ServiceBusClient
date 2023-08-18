using Confluent.Kafka;
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

public class SubscriberGrain : Grain, ISubscriberGrain
{
    private readonly ILogger _logger;

    public SubscriberGrain(ILogger<SubscriberGrain> logger) => _logger = logger;

    public string BootStrapServers = "localhost:29092";
    public string GroupId = "groupId1";
    
    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        _logger.LogError("SUBSCRIBER GRAIN HAS SHUT DOWN!!!!!! " + reason);
        return base.OnDeactivateAsync(reason, cancellationToken);
    }

    public void StartConsumer(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = BootStrapServers,
            GroupId = GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("topic1");

            try
            {
                while (true)
                {
                    _logger.LogInformation($"GETTING HUNGRY");
                    var consumeResult = consumer.Consume(cancellationToken);
                    _logger.LogInformation($"Consumed event from topic topic1 with key {consumeResult.Message.Key,-10} and value {consumeResult.Message.Value}");
                }
            }
            catch (Exception e) {
                _logger.LogError("KAFKA CONSUMER BLEW UP! " + e);
            }
            finally{
                _logger.LogError("CONSUMER IS CLOSING!!!");
                consumer.Close();
            }
        }
    }
    
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => StartConsumer(default(CancellationToken)));
        await base.OnActivateAsync(cancellationToken);
    }
 
    public Task DoWork(string topic, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Subscription Work Started: {IdentityString}");
        return Task.CompletedTask;
    }
    
    
}