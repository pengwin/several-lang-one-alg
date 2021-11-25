#ifndef __path_node
#define __path_node

class PathNode {
    private:
        PathNode *prev;
        int value;
    
    public:
        PathNode(int n, PathNode *prevNode) {
            value = n;
            prev = prevNode;
        }

        int Value() {
            return value;
        }

        PathNode *Prev() {
            return prev;
        }
};

#endif