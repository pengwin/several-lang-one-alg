#include <cstddef>
#include <vector>
#include <stdexcept>

#include <iostream>

#include "Node.hpp"
#include "Path.hpp"
#include "NodesInPathComparer.hpp"

#ifndef __nodes_qsort_sorting
#define __nodes_qsort_sorting

int Partition(NodesInPathComparer *comparer, std::vector<Node *> *nodes, int start, int end)
{
    Node *temp;
    int marker = start;
    for (int i = start; i < end; i++)
    {
        int compareResult = comparer->compare((*nodes)[i], (*nodes)[end]);
        if (compareResult == -1)
        {
            temp = (*nodes)[marker];
            (*nodes)[marker] = (*nodes)[i];
            (*nodes)[i] = temp;
            marker += 1;
        }
    }

    temp = (*nodes)[marker];
    (*nodes)[marker] = (*nodes)[end];
    (*nodes)[end] = temp;
    return marker;
}

void Quicksort(NodesInPathComparer *comparer, std::vector<Node *> *nodes, int start, int end)
{
    if (start >= end)
    {
        return;
    }

    int pivot = Partition(comparer, nodes, start, end);
    Quicksort(comparer, nodes, start, pivot - 1);
    Quicksort(comparer, nodes, pivot + 1, end);
}

class QSortNodesSorting
{
private:
    bool _usePath;
    PairsNotInPathCache *_cache;
    NodesInPathComparer *_comparer;

    void FlushCache()
    {
        if (_cache != NULL)
        {
            _cache->Flush();
        }
    }

public:
    QSortNodesSorting(Path *path, int maxN)
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
        _comparer = new NodesInPathComparer(_usePath, _cache);
    }

    ~QSortNodesSorting()
    {
        if (_cache != NULL)
        {
            delete _cache;
        }
    }

    void SortNodes(std::vector<Node *> *nodes)
    {
        FlushCache();
         Quicksort(_comparer, nodes, 0, nodes->size() -1);
    }
};

#endif