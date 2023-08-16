namespace GrainInterfaces;

public interface IHelloGrain : IGrainWithIntegerKey
{
    ValueTask<string> SayHello(string greeting);
}