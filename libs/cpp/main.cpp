#include <iostream>

#include "SquareSums.hpp"

int main() {
    for (int n = 2 ; n <= 2000 ; n++) {
        std::vector<int> res = square_sums_row(n);
        //std::cout << n << ": " << res.size() << "\n";
    }
}