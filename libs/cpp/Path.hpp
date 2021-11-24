#include <cstddef>
#include <vector>

#include "PathNode.hpp"

#ifndef __path
#define __path

class Path {
    private:
        PathNode *first;   
        PathNode *last;
        int count;
        std::vector<bool> attached;

        std::vector<int> toVector(PathNode *node, std::vector<int> v) {
            PathNode *prev = node->Prev();
            while (node != NULL) {
                v.push_back(node->Value());
                node = node->Prev();
            }
            return v;
        }

    public:
        Path(int capacity) {
            count = 0;
            last = NULL;
            attached = std::vector<bool>(capacity + 1);
            for(int i = 0; i < capacity + 1; i++) {
                attached[i] = false;
            }
        }

        ~Path() {
            PathNode *node = last;
            while (node != NULL){
                PathNode *prev = node->Prev();
                delete node;
                node = prev;
            }
            attached.clear();
        }

        bool Contains(int n) {
            return attached[n];
        }

        int Count() {
            return count;
        }

        void Push(int n) {
            if (attached[n]) {
                throw "Already attached";
            }

            PathNode *prev = last;
            last = new PathNode(n, prev);

            if (prev == NULL) {
                first = last;
            }

            attached[n] = true;
            count++;
        }

        void Pop() {
            if (last == NULL) {
                return;
            }
            attached[last->Value()] = false;
            PathNode *prev = last->Prev();
            delete last;
            last = prev;
            count--;
        }

        std::vector<int> ToVector() {
            return toVector(last, std::vector<int>());
        }


};

#endif