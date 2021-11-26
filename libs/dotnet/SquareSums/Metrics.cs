using System;
using System.Collections.Generic;

namespace SquareSums
{
    public class Metrics
    {
        private readonly bool _printProgress;
        private readonly Dictionary<int, int> _dfsCounterMap = new();
        
        private int _dfsCounter;
        
        public Metrics(bool printProgress)
        {
            _printProgress = printProgress;
        }

        public void IncrementDfsCounter()
        {
            _dfsCounter += 1;
        }

        public void FinalizeDfsCounter(int n)
        {
            _dfsCounterMap[n] = _dfsCounter;
            if (_printProgress)
            {
                Console.WriteLine($"Done: {n}: dfs counter {_dfsCounter}");
            }

            _dfsCounter = 0;
        }

        public void PrintMetrics()
        {
            var badCounter = 0;
            var normalCounter = 0;
            var averageCounter = 0;
            var maxN = 0;
            var worst = 0;
            var worstN = 0;
            foreach (var n in _dfsCounterMap.Keys)
            {
                if (n > maxN)
                {
                    maxN = n;
                }

                var dfsCounter = this._dfsCounterMap[n];
                if (dfsCounter > worst)
                {
                    worst = dfsCounter;
                    worstN = n;
                }

                if (dfsCounter / n > 3)
                {
                    //console.log(`Counter for ${n}: ${dfsCounter}`);
                    badCounter++;
                }
                else if (dfsCounter <= n)
                {
                    normalCounter++;
                }
                else
                {
                    averageCounter++;
                }
            }

            Console.WriteLine($"Normal (*<=n) dfs cases count: {normalCounter}/{maxN}");
            Console.WriteLine($"Average (n<*<3n) dfs cases count: {averageCounter}/{maxN}");
            Console.WriteLine($"Bad dfs (*>3n) cases count: {badCounter}/{maxN}");
            Console.WriteLine($"Worst dfs case: {worst} for {worstN}");
        }
    }
}