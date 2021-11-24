#include <vector>
#include <iostream>
#include <algorithm>

#ifndef __node
#define __node

class Node {
    private:
        int value;
        std::vector<Node*> pairs;

    public:
        Node(int val) {
            value = val;
            pairs = std::vector<Node*>();
        }

        ~Node() {
            pairs.clear();
        }

        int Value() {
            return value;
        }

        std::vector<Node*> Pairs() {
            return pairs;
        }

        void SortPairs() {
            std::sort(pairs.begin(), pairs.end(), Node::CompareNodes);
        }

        void Add(Node* node ) {
            pairs.push_back(node);
        }

        int PairsCount() {
            return pairs.size();
        }

        void PrintPairs() {
            std::cout << Value() << ":[";
            for (Node *p : pairs) {
                int v = p->Value();
                std::cout << v << ", ";
            }
            std::cout << "]\n";
        }

        static bool CompareNodes(Node *i, Node *j) 
        { 
            int a = i->PairsCount();
            int b = j->PairsCount();
            if (a != b) {
                return a < b;
            }

            a = i->Value();
            b = j->Value();
            
            return b < a;
        }
};

#endif