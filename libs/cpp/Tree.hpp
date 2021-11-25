#include <vector>

#include "Node.hpp"
#include "NodesSorting.hpp"

class Tree
{
private:
    std::vector<std::shared_ptr<Node>> nodes;

    std::shared_ptr<Node> getOrCreate(int n)
    {
        std::shared_ptr<Node> node = nodes[n - 1];
        if (node == NULL)
        {
            node = std::shared_ptr<Node>(new Node(n));
            nodes[n - 1] = node;
        }
        return node;
    }

public:
    Tree(int n)
    {
        nodes = std::vector<std::shared_ptr<Node>>(n);
    }

    ~Tree()
    {
        nodes.clear();
    }

    std::vector<std::shared_ptr<Node>> Roots()
    {
        return nodes;
    }

    void AddPair(int head, int tail)
    {
        std::shared_ptr<Node> headNode = getOrCreate(head);
        std::shared_ptr<Node> tailNode = getOrCreate(tail);
        headNode->Add(tailNode);
    }

    bool VerifyAllNodesHavePairs()
    {
        for (std::shared_ptr<Node> n : nodes)
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

    void SortPairsWithSorting(NodesSorting *sorting)
    {
        for (std::shared_ptr<Node> n : nodes)
        {
            sorting->SortNodes(n->Pairs());
        }
        sorting->SortNodes(&nodes);
    }
};