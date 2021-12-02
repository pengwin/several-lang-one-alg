#include <iostream>

#include "SquareSums.hpp"
#include "Metrics.hpp"
#include "NativeNodesSorting.hpp"
#include "QSortNodesSorting.hpp"

int main() {
    Metrics* metrics = new Metrics(false);
    /*int n = 8;
    std::vector<int> res = square_sums_row(n, metrics);
    std::cout << n << ": " << res.size() << "\n";*/
    for (int n = 2 ; n <= 2000 ; n++) {
        std::vector<int> res = square_sums_row<QSortNodesSorting>(n, metrics);
    }
    metrics->PrintMetrics();
    delete metrics;
}