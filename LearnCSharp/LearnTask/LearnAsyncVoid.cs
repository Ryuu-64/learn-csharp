namespace LearnCSharpLibrary.LearnTask;

public static class LearnAsyncVoid
{
    /// <summary>
    /// async void 方法只能直接调用而无法被 await
    /// 因为 void 不是 awaitable，而 Task 是 awaitable
    /// </summary>
    public static async Task InvokeAsyncVoid()
    {
        // Type 'void' is not awaitable
        // await TestAsyncVoid();
        AsyncVoid();

        // 给 async void 一点时间执行
        await Task.Delay(1000);
    }

    /// <summary>
    /// async Task 方法可以被 await，因为 Task 是 awaitable
    /// </summary>
    public static async Task InvokeAsyncTask()
    {
        await AsyncTask();
        // 不需要再 await Task.Delay(1000) 了，因为 async Task 可以被 await
    }

    /// <summary>
    /// async Task 方法中的异常可以被外部捕获
    /// </summary>
    public static async Task InvokeAsyncTaskWithTryCatch()
    {
        try
        {
            await AsyncTask();
        }
        catch (Exception ex)
        {
            Console.WriteLine("捕获到异常: " + ex.Message);
        }
    }

    /// <summary>
    /// async void 方法中的异常无法被外部捕获，因为异常没有地方可以储存
    /// 
    /// <see cref="InvokeTaskFromTaskCompletionSource"/>
    /// </summary>
    public static async Task InvokeAsyncVoidWithTryCatch()
    {
        try
        {
            AsyncVoid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("捕获到异常: " + ex.Message);
        }

        // 给 async void 一点时间执行
        await Task.Delay(1000);
    }

    private static async void AsyncVoid()
    {
        await Task.Delay(200);
        Console.WriteLine("async void 执行中...");
        throw new Exception("来自 async void 的异常");
    }

    private static async Task AsyncTask()
    {
        await Task.Delay(200);
        Console.WriteLine("async Task 执行中...");
        throw new Exception("来自 async Task 的异常");
    }

    public static async Task InvokeTaskFromTaskCompletionSource()
    {
        try
        {
            await TaskFromTaskCompletionSource();
        }
        catch (Exception e)
        {
            Console.WriteLine("捕获到异常: " + e.Message);
        }
    }

    private static Task TaskFromTaskCompletionSource()
    {
        var tcs = new TaskCompletionSource();
        try
        {
            throw new Exception("42");
        }
        catch (Exception ex)
        {
            tcs.SetException(ex);
        }

        return tcs.Task;
    }
}