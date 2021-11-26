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


        public int Value => _value;

        public IReadOnlyList<Node> Pairs => _pairs;

        
        public void SortPairsUsing(NodesSorting sorting)
        {
            _pairs = sorting.SortNodes(_pairs).ToList();
        }

        public void Add(Node node)
        {
            _pairs.Add(node);
        }

        public int PairsCount => _pairs.Count;

    };
}