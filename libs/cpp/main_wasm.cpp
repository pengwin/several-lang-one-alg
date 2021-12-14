#include <emscripten/emscripten.h>

#include "SquareSums.hpp"


int _FullSquareSums(int from, int to) {
    std::cout << "FullSquareSums\n";
    int count = 0;
    for (int n = from ; n <= to ; n++) {
        std::vector<int> res = square_sums_row(n);
        if (res.size() > 0) {
            count++;
        }
    }
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