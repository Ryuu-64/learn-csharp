namespace LearnCSharpLibrary.Foreach;

public readonly struct StructRange(int max)
{
    public struct Enumerator(int max)
    {
        private int current;

        public int Current => current;
        public bool MoveNext() => ++current <= max;
    }

    public Enumerator GetEnumerator() => new(max);
}