// See https://aka.ms/new-console-template for more information

using System;

namespace SquareSums
{
    class Program
    {
        public static void Main(string[] args)
        {
            int from = 2;
            int to = 2000;

            if (args.Length >= 2)
            {
                from = int.Parse(args[0]);
                to = int.Parse(args[1]);
            }
            
            Console.WriteLine("Calculating from: {0} to: {1}", from, to);
            
            var metrics = new Metrics(false);
            for (var n = from; n <= to; n++)
            {
                Calculator.SquareSumsRow(n, metrics);
            }

            metrics.PrintMetrics();
        }
    }
}






