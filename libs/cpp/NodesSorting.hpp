#include <cstddef>
#include <vector>
#include <stdexcept>

#include <iostream>

#include "Node.hpp"
#include "Path.hpp"

#ifndef __nodes_sorting
#define __nodes_sorting

// Sorting facade
template<class T>
class NodesSorting
{
private:
    T *_sorting;

public:
    NodesSorting(Path *path, int maxN)
    {
        _sorting = new T(path, maxN);
    }

    ~NodesSorting()
    {
        delete _sorting;
    }

    void SortNodes(std::vector<Node *> *nodes)
    {
        _sorting->SortNodes(nodes);
    }
};

#endif