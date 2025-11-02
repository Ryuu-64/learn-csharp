using System.Collections;

namespace LearnCSharpLibrary.Foreach;

public readonly struct MyStruct(int v1, int v2, int v3)
{
    public struct StructEnumerator(MyStruct data)
    {
        private int position = -1;

        public int Current => position switch
        {
            0 => data.Value1,
            1 => data.Value2,
            2 => data.Value3,
            _ => throw new InvalidOperationException()
        };

        public bool MoveNext()
        {
            position++;
            return position < 3;
        }
    }

    public readonly int Value1 = v1;
    public readonly int Value2 = v2;
    public readonly int Value3 = v3;

    public StructEnumerator GetEnumerator() => new(this);
}

public class MyClass(int v1, int v2, int v3)
{
    public int Value1 { get; } = v1;
    public int Value2 { get; } = v2;
    public int Value3 { get; } = v3;

    // 返回 IEnumerator<int>，这是普通迭代器
    public IEnumerator<int> GetEnumerator()
    {
        return new MyClassEnumerator(this);
    }

    private class MyClassEnumerator(MyClass data) : IEnumerator<int>
    {
        private int position = -1;

        public int Current => position switch
        {
            0 => data.Value1,
            1 => data.Value2,
            2 => data.Value3,
            _ => throw new InvalidOperationException()
        };

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            position++;
            return position < 3;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}