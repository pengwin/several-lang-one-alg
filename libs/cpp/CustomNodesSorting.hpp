#include <cstddef>
#include <vector>
#include <stdexcept>

#include <iostream>

#include "Node.hpp"
#include "Path.hpp"

#ifndef __custom_nodes_sorting
#define __custom_nodes_sorting

class SortBacketItem
{
private:
    Node *_value;

public:
    SortBacketItem(Node *value)
    {
        _value = value;
    }

    Node *Value()
    {
        return _value;
    }
};

class SortBucketList
{
private:
    Path *_path;
    std::vector<SortBacketItem *> *_list;

public:
    SortBucketList(Path *path)
    {
        _list = new std::vector<SortBacketItem *>();
        _path = path;
    }

    ~SortBucketList()
    {
        for (SortBacketItem *item : *_list)
        {
            delete item;
        }
        delete _list;
    }

    std::vector<SortBacketItem *> *List()
    {
        return _list;
    }

    void AddNode(Node *node)
    {
        bool found = false;
        for (int i = 0; i < _list->size(); i++)
        {
            Node *currentNode = _list->at(i)->Value();
            bool condition;
            if (_path != NULL)
            {
                auto a = node->PairsCount();
                auto b = currentNode->PairsCount();
                if (a != b)
                {
                    condition = a < b;
                }
                else
                {
                    condition = node->Value() > currentNode->Value();
                }
            }
            else
            {
                condition = node->Value() > currentNode->Value();
            }

            if (condition)
            {
                auto pos = _list->begin() + i;
                _list->insert(pos, new SortBacketItem(node));
                found = true;
                break;
            }
        }

        // reached end of list without adding
        if (!found)
        {
            _list->push_back(new SortBacketItem(node));
        }
    }
};

class SortListContainer
{
private:
    Path *_path;

public:
    std::vector<SortBucketList *> *SortList;

    SortListContainer(Path *path, int maxN)
    {
        _path = path;
        SortList = new std::vector<SortBucketList *>(maxN + 1);
    }

    ~SortListContainer()
    {
        for (SortBucketList *list : *SortList)
        {
            delete list;
        }
        delete SortList;
    }

    void AddNode(Node *node)
    {
        auto pairsCount = _path == NULL ? node->PairsCount() : node->PairsNotInPath(_path);

        auto list = SortList->at(pairsCount);
        if (list == NULL)
        {
            list = new SortBucketList(_path);
        }

        list->AddNode(node);

        (*SortList)[pairsCount] = list;
    }
};


class CustomNodesSorting
{
private:
    Path* _path;
    int _maxN;
    

public:
    CustomNodesSorting(Path *path, int maxN)
    {
        if (path != NULL)
        {
            _path = path;
        }
        else
        {
            _path = NULL;
        }
        _maxN = maxN;
    }

    void SortNodes(std::vector<Node *> *nodes)
    {
        auto sortList = new SortListContainer(_path, _maxN);

        for (Node *node : *nodes)
        {
            sortList->AddNode(node);
        }

        int index = 0;
        for (auto list : *(sortList->SortList))
        {
            if (list == NULL)
            {
                continue;
            }

            for (auto item : *list->List())
            {
                (*nodes)[index] = item->Value();
                index++;
            }
        }

        delete sortList;
    }
};

#endif