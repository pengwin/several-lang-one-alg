#include <cstddef>
#include <vector>
#include <stdexcept>

#include <iostream>

#include "Node.hpp"
#include "Path.hpp"
#include "NodesInPathComparer.hpp"

#ifndef __nodes_native_sorting
#define __nodes_native_sorting

class NativeNodesSorting
{
private:
    bool _usePath;
    PairsNotInPathCache *_cache;

    void FlushCache()
    {
        if (_cache != NULL)
        {
            _cache->Flush();
        }
    }

public:
    NativeNodesSorting(Path *path, int maxN)
    {
        _usePath = false;
        if (path != NULL)
        {
            _usePath = true;
            _cache = new PairsNotInPathCache(path, maxN);
        }
        else
        {
            _cache = NULL;
        }
    }

    ~NativeNodesSorting()
    {
        if (_cache != NULL)
        {
            delete _cache;
        }
    }

    void SortNodes(std::vector<Node *> *nodes)
    {
        FlushCache();
        std::sort(nodes->begin(), nodes->end(), NodesInPathComparer(_usePath, _cache));
    }
};

#endif