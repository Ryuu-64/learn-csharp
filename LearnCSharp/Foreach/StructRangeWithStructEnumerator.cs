namespace LearnCSharpLibrary.Foreach;

public readonly struct StructRangeWithStructEnumerator(int max)
{
    public struct StructEnumerator(int max)
    {
        private int current;

        public void Reset()
        {
            current = 0;
        }

        public int Current => current;
        public bool MoveNext() => ++current <= max;

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }

    public StructEnumerator GetEnumerator() => new StructEnumerator(max);
}