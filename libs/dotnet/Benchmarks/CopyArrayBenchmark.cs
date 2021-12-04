using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using SquareSums;

namespace Benchmarks
{
    [MemoryDiagnoser(true)]
    public class CopyArrayBenchmark
    {
        private static T[] CopySourceImpl<T>(List<T> source)
        {
            var sourceSpan = CollectionsMarshal.AsSpan(source);
            var result = new T[sourceSpan.Length];
            sourceSpan.CopyTo(result.AsSpan());
            return result;
        }

        private static List<int> List;

        static CopyArrayBenchmark()
        {
            var size = 200;
            List = new List<int>(size);
            for (var index = 0; index < size; index++)
            {
                List.Add(index*index);
            }
        }

        [Benchmark]
        public int[] ToArray() => List.ToArray();

        [Benchmark]
        public void CopySource() => CopySourceImpl(List);
    }
}