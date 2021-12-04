// See https://aka.ms/new-console-template for more information

using System;
using BenchmarkDotNet.Running;
using Benchmarks;

var summary = BenchmarkRunner.Run<CopyArrayBenchmark>();