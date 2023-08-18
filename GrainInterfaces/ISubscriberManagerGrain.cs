namespace GrainInterfaces;

public interface ISubscriberManagerGrain : IGrainWithIntegerKey
{
    Task DoWork();
}