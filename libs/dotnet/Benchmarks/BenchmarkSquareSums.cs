using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SquareSums;

namespace Benchmarks
{
    [SimpleJob(RunStrategy.Monitoring)]
    public class BenchmarkSquareSums
    {
        [Benchmark]
        public Span<int>  SquareSumsRow102() => Calculator.SquareSumsRow(102, null);

        [Benchmark]
        public void SquareSumsRow1000_2000()
        {
            for (var i = 1000; i < 1100; i++)
            {
                Calculator.SquareSumsRow(i, null);
            }
        }
    }
}