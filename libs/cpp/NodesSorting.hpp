#include <cstddef>
#include <vector>
#include <stdexcept>
#include <memory>

#include "Node.hpp"
#include "Path.hpp"

#ifndef __nodes_sorting
#define __nodes_sorting

class SortBucket
{
public:
    std::shared_ptr<Node> Value;

    std::shared_ptr<SortBucket> Next;

    SortBucket(std::shared_ptr<Node> value)
    {
        Value = value;
    }
};

class SortBucketList
{
private:
    std::shared_ptr<Path>_path;

public:
    std::shared_ptr<SortBucket> First;

    SortBucketList(std::shared_ptr<Path>path)
    {
        _path = path;
    }

    void AddNode(std::shared_ptr<Node> node)
    {
        if (First == NULL)
        {
            First = std::shared_ptr<SortBucket>(new SortBucket(node));
            return;
        }

        auto current = First;
        std::shared_ptr<SortBucket> prev = NULL;
        while (current != NULL)
        {
            bool condition;
            if (_path != NULL)
            {
                auto a = node->PairsCount();
                auto b = current->Value->PairsCount();
                if (a != b)
                {
                    condition = a < b;
                }
                else
                {
                    condition = node->Value() > current->Value->Value();
                }
            }
            else
            {
                condition = node->Value() > current->Value->Value();
            }

            if (condition)
            {
                auto next = current;
                current = std::shared_ptr<SortBucket>(new SortBucket(node));
                current->Next = next;

                if (prev != NULL)
                {
                    prev->Next = current;
                }
                else
                {
                    First = current;
                }
                break;
            }

            prev = current;
            current = current->Next;
        }

        // reached end of list without adding
        if (current == NULL && prev != NULL)
        {
            prev->Next = std::shared_ptr<SortBucket>(new SortBucket(node));
        }
    }
};

int pairsNotInPath(std::shared_ptr<Node> n, std::shared_ptr<Path>path)
{
    int count = 0;
    for (std::shared_ptr<Node> p : *n->Pairs())
    {
        if (p == NULL)
        {
            throw std::invalid_argument("Received unexpected null node");
        }
        if (path->Contains(p->Value()))
        {
            continue;
        }
        count++;
    }
    return count;
}

class NodesSorting
{
private:
    std::shared_ptr<Path>_path;
    int _maxN;

public:
    NodesSorting(std::shared_ptr<Path>path, int maxN)
    {
        _path = path;
        _maxN = maxN;
    }

public:
    void SortNodes(std::vector<std::shared_ptr<Node>> *nodes)
    {
        auto sortList = std::vector<SortBucketList *>(_maxN + 1);

        for (std::shared_ptr<Node> node : *nodes)
        {
            auto pairsCount = _path == NULL ? node->PairsCount() : pairsNotInPath(node, _path);

            auto list = sortList[pairsCount];
            if (list == NULL)
            {
                list = new SortBucketList(_path);
            }

            list->AddNode(node);

            sortList[pairsCount] = list;
        }

        int index = 0;
        for (auto list : sortList)
        {
            if (list == NULL)
            {
                continue;
            }

            auto current = list->First;
            while (current != NULL)
            {
                (*nodes)[index] = current->Value;
                index++;
                current = current->Next;
            }
        }

        sortList.clear();
    }
};

#endif