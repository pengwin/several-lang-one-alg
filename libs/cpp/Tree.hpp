#include <vector>
#include <iostream>

#include "Node.hpp"
#include "NodesSorting.hpp"

template<class T>
class Tree
{
private:
    std::vector<Node *>*nodes;

    Node *getOrCreate(int n)
    {
        Node *node = nodes->at(n - 1);
        if (node == NULL)
        {
            node = new Node(n);
            (*nodes)[n - 1] = node;
        }
        return node;
    }

public:
    Tree(int n)
    {
        nodes = new std::vector<Node *>(n);
    }

    ~Tree()
    {
        for(Node *node : *(nodes)) {
            delete node;
        }
        nodes->clear();
        delete nodes;
    }

    std::vector<Node *>* Roots()
    {
        return nodes;
    }

    void AddPair(int head, int tail)
    {
        Node *headNode = getOrCreate(head);
        Node *tailNode = getOrCreate(tail);
        headNode->Add(tailNode);
    }

    bool VerifyAllNodesHavePairs()
    {
        for (Node *n : *nodes)
        {
            if (n == NULL)
            {
                return false;
            }

            if (n->PairsCount() == 0)
            {
                return false;
            }
        }

        return true;
    }

    void SortPairsWithSorting(NodesSorting<T> *sorting)
    {
        for (Node *n : *nodes)
        {
            sorting->SortNodes(n->Pairs());
        }
        sorting->SortNodes(nodes);
    }
};