using LearnCSharp.LearnTask;

namespace LearnCSharpTest.LearnTask;

[TestFixture]
[TestOf(typeof(LearnAsyncVoid))]
public class LearnAsyncVoidTest
{
    [Test]
    public void InvokeAsyncTask()
    {
        Task.WaitAll(
            LearnAsyncVoid.InvokeAsyncTask(),
            LearnAsyncVoid.InvokeAsyncTaskWithTryCatch()
        );
    }

    [Test]
    public void InvokeAsyncVoid()
    {
        Task.WaitAll(
            LearnAsyncVoid.InvokeAsyncVoid(),
            LearnAsyncVoid.InvokeAsyncVoidWithTryCatch()
        );
    }


    [Test]
    public void InvokeTaskFromTaskCompletionSource()
    {
        LearnAsyncVoid.InvokeTaskFromTaskCompletionSource().Wait();
    }
}