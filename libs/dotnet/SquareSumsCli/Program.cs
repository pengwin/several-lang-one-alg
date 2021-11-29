// See https://aka.ms/new-console-template for more information

using SquareSums;

var metrics = new Metrics(false);
for (var n = 500; n <= 1000; n++)
{
    Calculator.SquareSumsRow(n, metrics, NodesSorting.CreateNativeSorting);
}

metrics.PrintMetrics();


