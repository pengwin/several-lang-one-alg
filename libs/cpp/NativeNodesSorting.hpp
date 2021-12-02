#include <cstddef>
#include <vector>
#include <stdexcept>

#include <iostream>

#include "Node.hpp"
#include "Path.hpp"

#ifndef __native_nodes_sorting
#define __native_nodes_sorting


class PairsNotInPathCache
{
private:
    Path *_path;
    std::vector<int> *_cache;

public:
    PairsNotInPathCache(Path *p, int maxN)
    {
        _path = p;
        _cache = new std::vector<int>(maxN + 1);
    }

    ~PairsNotInPathCache()
    {
        if (_cache != NULL)
        {
            delete _cache;
        }
    }

    void Flush()
    {
        if (_path == NULL)
        {
            return;
        }
        int size = _cache->size();
        _cache->assign(size, -1);
    }

    int GetPairsNotInPath(Node *node)
    {
        int index = node->Value();
        int result = _cache->at(index);
        if (result == -1)
        {
            result = node->PairsNotInPath(_path);
            (*_cache)[index] = result;
        }
        return result;
    }
};

struct NodesInPathComparer
{
private:
    bool _usePath;
    PairsNotInPathCache *_cache;

public:
    NodesInPathComparer(bool usePath, PairsNotInPathCache *cache)
    {
        _usePath = usePath;
        _cache = cache;
    }

    bool operator()(Node *i, Node *j)
    {
        if (_usePath)
        {
            int a = _cache->GetPairsNotInPath(i);
            int b = _cache->GetPairsNotInPath(j);
            if (a != b)
            {
                return a < b;
            }
        }

        int a = i->PairsCount();
        int b = j->PairsCount();
        if (a != b)
        {
            return a < b;
        }

        a = i->Value();
        b = j->Value();
        return a > b;
    }
};

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