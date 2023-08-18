namespace GrainInterfaces;

public interface ISubscriberManagerGrain : IGrainWithIntegerKey
{
    Task DoWork(string topic, CancellationToken cancellationToken);
}