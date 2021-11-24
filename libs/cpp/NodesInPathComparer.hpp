#include "Node.hpp"
#include "Path.hpp"


int pairsNotInPath(Node* n, Path* path) {
    int count = 0;
    for (Node *p : n->Pairs()) {
        if (path->Contains(p->Value())){
            continue;
        }
        count++;
    }
    return count;
}


struct NodesInPathComparer {
    private:
        Path *path;
    public:

        NodesInPathComparer(Path *p) {
            path = p;
        }

        bool operator() (Node* i, Node* j) {
            int a = pairsNotInPath(i, path);
            int b = pairsNotInPath(j, path);
            if (a != b) {
                return a < b;
            }
            
            a = i->PairsCount();
            b = j->PairsCount();
            if (a != b) {
                return a < b;
            }

            a = i->Value();
            b = j->Value();
            return a < b;
        }
};