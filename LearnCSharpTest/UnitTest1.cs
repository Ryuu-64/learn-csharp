using LearnCSharp;

namespace LearnCSharpTest;

public class LearnTaskStateMachineTests
{
    [Test]
    public async Task Test1()
    {
        await LearnTaskStateMachine.StartAsync();
        Assert.Pass();
    }
}