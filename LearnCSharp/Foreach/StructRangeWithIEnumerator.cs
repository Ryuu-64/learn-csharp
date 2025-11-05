using System.Collections;

namespace LearnCSharpLibrary.Foreach;

public readonly struct StructRangeWithIEnumerator(int max)
{
    public struct StructEnumerator(int max) : IEnumerator<int>
    {
        private int current;

        public void Reset()
        {
            current = 0;
        }

        object? IEnumerator.Current => Current;

        public int Current => current;
        public bool MoveNext() => ++current <= max;

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }

    /// <summary>
    /// Boxing allocation: conversion from <see cref="StructEnumerator"/> to <see cref="IEnumerator{int}"/> requires boxing of the value type
    /// 由于 <see cref="IEnumerator{int}"/> 的类型是接口，是引用类型的，此时不得不把 <see cref="StructEnumerator"/> 装箱。
    /// 装箱转换：.NET 运行时为了保证 <see cref="StructEnumerator"/> 变为一个引用类型会在托管堆分配内存，然后将栈上的 <see cref="StructEnumerator"/> 完整的复制到这个内存中，最终返回内存地址的引用。
    /// </summary>
    public IEnumerator<int> GetEnumerator() => new StructEnumerator(max);
}