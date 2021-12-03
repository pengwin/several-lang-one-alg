using System;
using BenchmarkDotNet.Attributes;
using SquareSums;

namespace Benchmarks
{
    public class IsFairSquareBenchmark
    {
        static bool IsFairSquare(int n)
        {
            double sqrtVal = Math.Sqrt(n);
            return sqrtVal - Math.Floor(sqrtVal) == 0;
        }

        [Benchmark]
        public void BitCheckImpl()
        {
            for (var i = 0; i < 2000; i++)
            {
                Calculator.IsFairSquare(i);
            }
        }
        
        [Benchmark]
        public void MathImpl()
        {
            for (var i = 0; i < 2000; i++)
            {
                IsFairSquare(i);
            }
        }
    }
}