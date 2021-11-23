#include <emscripten/emscripten.h>

#include "SquareSums.hpp"

#ifdef __cplusplus
extern "C" {
#endif

EMSCRIPTEN_KEEPALIVE int FullCalc(int from, int to) {
    int count = 0;
    for (int n = from ; n <= to ; n++) {
        std::vector<int> res = square_sums_row(n);
        if (res.size() > 0) {
            count++;
        }
        std::cout << n << ": " << res.size() << "\n";
    }
    return count;
}

#ifdef __cplusplus
}
#endif