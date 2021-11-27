﻿// See https://aka.ms/new-console-template for more information

using SquareSums;

var metrics = new Metrics(false);
for (var n = 2; n <= 2000; n++)
{
    Calculator.SquareSumsRow(n, metrics, NodesSorting.CreateNativeSorting);
}

metrics.PrintMetrics();


