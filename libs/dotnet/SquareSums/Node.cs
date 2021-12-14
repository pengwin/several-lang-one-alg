using System.Collections.Generic;

namespace SquareSums
{
    internal class Node {
    
        private readonly int _value;
        private readonly List<Node> _pairs;

    
        public Node(int val) {
            _value = val;
            _pairs = new List<Node>();
        }
    

        public int Value() {
            return _value;
        }

        public IReadOnlyList<Node> Pairs() {
            return _pairs;
        }

        public void SortPairs() {
            _pairs.Sort(CompareNodes);
        }

        public void Add(Node node ) {
            _pairs.Add(node);
        }

        public int PairsCount() {
            return _pairs.Count;
        }

        /*void PrintPairs() {
            std::cout << Value() << ":[";
            for (Node *p : pairs) {
                int v = p.Value();
                std::cout << v << ", ";
            }
            std::cout << "]\n";
        }*/

        public static int CompareNodes(Node? i, Node? j) 
        {
            if (i == null || j == null)
            {
                return 0;
            }
        
            int a = i.PairsCount();
            int b = j.PairsCount();

            if (a < b)
            {
                return -1;
            }

            return 1;
        }
    };
}