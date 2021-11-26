#include <iostream>
#include <map>

#ifndef __metrics
#define __metrics

class Metrics
    {
        private:  
        bool _printProgress;
    std::map<int,int>* _dfsCounterMap;
        
         int _dfsCounter;

         public:
        
        Metrics(bool printProgress)
        {
            _printProgress = printProgress;
            _dfsCounter = 0;
            _dfsCounterMap = new std::map<int,int>();
        }

        ~Metrics() 
        {
            delete _dfsCounterMap;
        }

        void IncrementDfsCounter()
        {
            _dfsCounter += 1;
        }

        void FinalizeDfsCounter(int n)
        {
            (*_dfsCounterMap)[n] = _dfsCounter;
            if (_printProgress)
            {
                std::cout << "Done: " << n << ": dfs counter " << _dfsCounter << "\n";
            }

            _dfsCounter = 0;
        }

        void PrintMetrics()
        {
            int badCounter = 0;
            int normalCounter = 0;
            int averageCounter = 0;
            int maxN = 0;
            int worst = 0;
            int worstN = 0;
            for (const auto& [n, dfsCounter] : *_dfsCounterMap)
            {
                if (n > maxN)
                {
                    maxN = n;
                }

                if (dfsCounter > worst)
                {
                    worst = dfsCounter;
                    worstN = n;
                }

                if (dfsCounter / n > 3)
                {
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

            std::cout << "Normal (*<=n) dfs cases count: " << normalCounter << "/" << maxN << "\n";
            std::cout << "Average (n<*<3n) dfs cases count: " << averageCounter<< "/" <<maxN<< "\n";
            std::cout << "Bad dfs (*>3n) cases count: " << badCounter<< "/" <<maxN<< "\n";
            std::cout << "Worst dfs case: " << worst << " for " << worstN << "\n";
        }
    };

    #endif