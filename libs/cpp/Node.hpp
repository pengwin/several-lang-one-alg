#include <vector>
#include <iostream>
#include <algorithm>
#include <memory>

#ifndef __node
#define __node

class Node {
    private:
        int value;
        std::vector<std::shared_ptr<Node>> pairs;

    public:
        Node(int val) {
            value = val;
            pairs = std::vector<std::shared_ptr<Node>>();
        }

        ~Node() {
            pairs.clear();
        }

        int Value() {
            return value;
        }

        std::vector<std::shared_ptr<Node>>* Pairs() {
            return &pairs;
        }

        void Add(std::shared_ptr<Node> node ) {
            pairs.push_back(node);
        }

        int PairsCount() {
            return pairs.size();
        }

        void PrintPairs() {
            std::cout << Value() << ":[";
            for (std::shared_ptr<Node>p : pairs) {
                int v = p->Value();
                std::cout << v << ", ";
            }
            std::cout << "]\n";
        }

        static bool CompareNodes(std::shared_ptr<Node>i, std::shared_ptr<Node>j) 
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