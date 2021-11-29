#include <iostream>

#include "SquareSums.hpp"
#include "Metrics.hpp"

int main() {
    Metrics* metrics = new Metrics(false);
    /*int n = 8;
    std::vector<int> res = square_sums_row(n, metrics);
    std::cout << n << ": " << res.size() << "\n";*/
    for (int n = 500 ; n <= 1000 ; n++) {
        std::vector<int> res = square_sums_row(n, metrics);
    }
    metrics->PrintMetrics();
    delete metrics;
}