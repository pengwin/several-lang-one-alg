#include <emscripten/emscripten.h>

#include "SquareSums.hpp"
#include "Metrics.hpp"
#include "NativeNodesSorting.hpp"
#include "QSortNodesSorting.hpp"


int _FullSquareSums(int from, int to) {
    std::cout << "FullSquareSums\n";
    int count = 0;
    Metrics* metrics = new Metrics(false);
    for (int n = from ; n <= to ; n++) {
        std::vector<int> res = square_sums_row<NativeNodesSorting>(n, metrics);
        if (res.size() > 0) {
            count++;
        }
    }
    metrics->PrintMetrics();
    delete metrics;
    return count;
}

#ifdef __cplusplus
extern "C" {
#endif

EMSCRIPTEN_KEEPALIVE int FullSquareSums(int from, int to) {
    return _FullSquareSums(from, to);
}

#ifdef __cplusplus
}
#endif