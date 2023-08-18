namespace GrainInterfaces;

public interface IDirectorGrain : IGrainWithIntegerKey
{
    Task DoWork();
}