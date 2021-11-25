using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{
    internal class Node
    {
        private readonly int _value;
        private List<Node> _pairs;


        public Node(int val)
        {
            _value = val;
            _pairs = new List<Node>();
        }


        public int Value()
        {
            return _value;
        }

        public IReadOnlyList<Node> Pairs()
        {
            return _pairs;
        }

        /*public void SortPairs()
        {
            _pairs.Sort(CompareNodes);
        }*/
        
        public void SortPairsUsing(NodesSorting sorting)
        {
            _pairs = sorting.SortNodes(_pairs).ToList();
        }

        public void Add(Node node)
        {
            _pairs.Add(node);
        }

        public int PairsCount()
        {
            return _pairs.Count;
        }

        public static int CompareNodes(Node? i, Node? j)
        {
            if (i == null || j == null)
            {
                return 0;
            }

            int a = i.PairsCount();
            int b = j.PairsCount();

            if (a != b)
            {
                return a.CompareTo(b);
            }

            return j.Value().CompareTo(i.Value());
        }
    };
}