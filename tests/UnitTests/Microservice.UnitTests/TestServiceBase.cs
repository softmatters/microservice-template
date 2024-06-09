using Moq.AutoMock;

namespace Microservice.UnitTests;

public class TestServiceBase
{
    public AutoMocker Mocker { get; }

    public TestServiceBase()
    {
        Mocker = new AutoMocker();
    }
}