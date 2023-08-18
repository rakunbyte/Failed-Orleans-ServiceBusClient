namespace GrainInterfaces;

public interface ISubscriberGrain : IGrainWithIntegerKey
{
    Task DoWork(string topic, CancellationToken cancellationToken);
}