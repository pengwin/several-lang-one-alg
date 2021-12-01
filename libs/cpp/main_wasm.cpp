#include <emscripten/emscripten.h>

#include "SquareSums.hpp"
#include "Metrics.hpp"

#ifdef __cplusplus
extern "C" {
#endif

EMSCRIPTEN_KEEPALIVE int FullSquareSums(int from, int to) {
    int count = 0;
    Metrics* metrics = new Metrics(false);
    for (int n = from ; n <= to ; n++) {
        std::vector<int> res = square_sums_row(n, metrics);
        if (res.size() > 0) {
            count++;
        }
        //std::cout << n << ": " << res.size() << "\n";
    }
    metrics->PrintMetrics();
    delete metrics;
    return count;
}

#ifdef __cplusplus
}
#endif