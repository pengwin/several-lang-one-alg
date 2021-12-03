using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable ForCanBeConvertedToForeach

namespace SquareSums
{
    public class Node
    {
        /// <summary>
        /// Used while constructing tree
        /// </summary>
        private readonly List<Node> _buildTimePairs;
        private Node[] _pairs;
        
        public Node(int val)
        {
            Value = val;
            _buildTimePairs = new List<Node>(val);
            _pairs = Array.Empty<Node>();
        }


        public int Value { get; }
        
        public Span<Node> Pairs => _pairs.AsSpan();

        
        public void Add(Node node)
        {
            _buildTimePairs.Add(node);
        }

        public void FinalizePairs()
        {
            var sourceSpan = CollectionsMarshal.AsSpan(_buildTimePairs);
            _pairs = new Node[sourceSpan.Length];
            sourceSpan.CopyTo(_pairs.AsSpan());
        }

        public int PairsCount => _pairs.Length;
    };
}