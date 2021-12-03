using System;
using System.Collections.Generic;
// ReSharper disable ForCanBeConvertedToForeach

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
        
        public void SortPairsUsing(NodesSortingFacade sorting)
        {
            for (var i = 0; i < _nodes.Length; i++)
            {
                var n = _nodes[i];
                if (n == null)
                {
                    throw new Exception("Unexpected null node");
                }

                sorting.SortNodes(n.Pairs);
            }

            sorting.SortNodes(_nodes.AsSpan()!);
        }

        public bool FinalizePairsAndVerifyAllNodesHavePairs()
        {
            for (var i = 0; i < _nodes.Length; i++)
            {
                var node = _nodes[i];
                if (node == null)
                {
                    return false;
                }
                
                node.FinalizePairs();
                
                if (node.PairsCount == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}