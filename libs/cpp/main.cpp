#include <iostream>

#include "SquareSums.hpp"
#include "Metrics.hpp"

int main() {
    Metrics* metrics = new Metrics(false);
    /*int n = 50;
    std::vector<int> res = square_sums_row(n);
    std::cout << n << ": " << res.size() << "\n";*/
    for (int n = 2 ; n <= 2000 ; n++) {
        std::vector<int> res = square_sums_row(n, metrics);
        //std::cout << n << ": " << res.size() << "\n";
    }
    metrics->PrintMetrics();
    delete metrics;
}