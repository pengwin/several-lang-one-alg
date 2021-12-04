#include <iostream>

#include "SquareSums.hpp"
#include "Metrics.hpp"
#include "NativeNodesSorting.hpp"
#include "QSortNodesSorting.hpp"

int main(int argc, char** argv) {

    int from = 2;
    int to = 2000;
    if (argc >= 3) {
        from = atoi(argv[1]);
        to = atoi(argv[2]);
    }

    std::cout << "Calculating from: " << from  << " to:" << to << "\n";

    for (int i = 0; i < argc; ++i)
        std::cout << argv[i] << "\n";

    Metrics* metrics = new Metrics(false);
    for (int n = from ; n <= to ; n++) {
        std::vector<int> res = square_sums_row<NativeNodesSorting>(n, metrics);
    }
    metrics->PrintMetrics();
    delete metrics;
}