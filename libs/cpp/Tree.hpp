#include <vector>

#include "Node.hpp"


class Tree {
    private:
        std::vector<Node*> nodes;

        Node* getOrCreate(int n) {
            Node *node = nodes[n-1];
            if (node == NULL) {
                node = new Node(n);
                nodes[n-1] = node;
            }
            return node;
        }

    public:
        Tree(int n) {
            nodes = std::vector<Node*>(n);
        }

        ~Tree() {
            for (Node *n : nodes) {
                delete n;
            }
            nodes.clear();
        }

        std::vector<Node*> Roots() {
            return nodes;
        }

        void AddPair(int head, int tail) {
            Node *headNode = getOrCreate(head);
	        Node *tailNode = getOrCreate(tail);
            headNode->Add(tailNode);
        }

        bool VerifyAllNodesHavePairs() {
            for (Node *n : nodes) {
                if (n == NULL) {
                    return false;
                }

                if (n->PairsCount() == 0) {
                    return false;
                }
            }

            return true;
        }

        void SortPairs() {
            for (Node *n : nodes) {
                n->SortPairs();
            }
            std::sort(nodes.begin(), nodes.end(), Node::CompareNodes);
        }

};