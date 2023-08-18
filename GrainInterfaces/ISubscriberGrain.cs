namespace GrainInterfaces;

public interface ISubscriberGrain : IGrainWithIntegerKey
{
    Task DoWork();
}