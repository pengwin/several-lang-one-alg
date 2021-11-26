#include <vector>
#include <iostream>
#include <algorithm>
 

#ifndef __node
#define __node

class Node {
    private:
        int value;
        std::vector<Node*>* pairs;

    public:
        Node(int val) {
            value = val;
            pairs = new std::vector<Node*>();
        }

        ~Node() {
            pairs->clear();
            delete pairs;
        }

        int Value() {
            return value;
        }

        std::vector<Node*>* Pairs() {
            return pairs;
        }

        void Add(Node* node ) {
            pairs->push_back(node);
        }

        int PairsCount() {
            return pairs->size();
        }

        void PrintPairs() {
            std::cout << Value() << ":[";
            for (Node* p : *pairs) {
                int v = p->Value();
                std::cout << v << ", ";
            }
            std::cout << "]\n";
        }
};

#endif