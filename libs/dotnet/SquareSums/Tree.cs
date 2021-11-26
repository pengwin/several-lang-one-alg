using System;
using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{
    internal class Tree
    {
        private Node?[] _nodes;

        private Node GetOrCreate(int n)
        {
            var node = _nodes[n - 1];
            if (node == null)
            {
                node = new Node(n);
                _nodes[n - 1] = node;
            }

            return node;
        }

        public Tree(int n)
        {
            _nodes = new Node?[n];
        }

        public IReadOnlyList<Node?> Roots()
        {
            return _nodes;
        }

        public void AddPair(int head, int tail)
        {
            Node headNode = GetOrCreate(head);
            Node tailNode = GetOrCreate(tail);
            headNode.Add(tailNode);
        }

        public bool VerifyAllNodesHavePairs()
        {
            foreach (var n in _nodes)
            {
                if (n == null)
                {
                    return false;
                }

                if (n.PairsCount == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /*public void SortPairs()
        {
            foreach (var n in _nodes)
            {
                n.SortPairs();
            }

            Array.Sort(_nodes, Node.CompareNodes);
        }*/

        public void SortPairsUsing(NodesSorting sorting)
        {
            foreach (var n in _nodes)
            {
                if (n == null)
                {
                    throw new Exception("Unexpected null node");
                }
                n.SortPairsUsing(sorting);
            }

            _nodes = sorting.SortNodes(_nodes!).ToArray();
        }
    }
}