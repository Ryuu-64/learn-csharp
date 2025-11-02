using LearnCSharpLibrary.Foreach;

namespace LearnCSharpConsole;

internal static class Program
{
    public static void Main()
    {
        StartStructEnum();
        StartStructEnumBenchmark();
        StartClassEnumBenchmark();
    }

    private static void StartStructEnumBenchmark()
    {
        // 1. 清理 GC 并记录初始分配
        GC.Collect();
        long before = GC.GetAllocatedBytesForCurrentThread();

        // 2. foreach 遍历 struct
        foreach (int i in new MyStruct(10, 20, 30))
        {
            _ = i; // 避免 Console.WriteLine 对堆分配干扰
        }

        // 3. 手动 enumerator 遍历
        var enumerator = new MyStruct(10, 20, 30).GetEnumerator();
        while (enumerator.MoveNext())
        {
            _ = enumerator.Current;
        }

        long after = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"Allocated bytes delta = {after - before}");
    }

    private static void StartClassEnumBenchmark()
    {
        // 1. 清理 GC 并记录初始分配
        GC.Collect();
        long before = GC.GetAllocatedBytesForCurrentThread();

        var obj = new MyClass(10, 20, 30);

        // 2. foreach 遍历 class iterator
        foreach (int i in obj)
        {
            _ = i; // 避免 Console.WriteLine 对堆分配干扰
        }

        // 3. 手动 enumerator 遍历
        var enumerator = obj.GetEnumerator();
        while (enumerator.MoveNext())
        {
            _ = enumerator.Current;
        }

        long after = GC.GetAllocatedBytesForCurrentThread();

        Console.WriteLine($"Allocated bytes delta = {after - before}");
    }


    private static void StartStructEnum()
    {
        GC.Collect();
        GC.TryStartNoGCRegion(1024 * 1024);

        long before = GC.GetAllocatedBytesForCurrentThread();

        var sum = 0;
        foreach (int i in new StructRange(5))
        {
            sum += i;
        }

        long after = GC.GetAllocatedBytesForCurrentThread();

        GC.EndNoGCRegion();

        Console.WriteLine($"Sum = {sum}");
        Console.WriteLine($"Allocated bytes delta = {after - before}");
    }
}