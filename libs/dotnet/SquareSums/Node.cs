using System;
using System.Collections.Generic;
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
        private int[] _values;


        public Node(int val)
        {
            Value = val;
            _buildTimePairs = new List<Node>(val);
            _pairs = Array.Empty<Node>();
            _values = Array.Empty<int>();
        }


        public int Value { get; }

        public Span<int> Values => _values;
        
        public Span<Node> Pairs => _pairs;

        
        public void Add(Node node)
        {
            _buildTimePairs.Add(node);
        }

        public void FinalizePairs()
        {
            _pairs = _buildTimePairs.ToArray();
            _values = new int[_pairs.Length];
            for (int i = 0; i < _pairs.Length; i++)
            {
                _values[i] = _pairs[i].Value;
            }
        }

        public int PairsCount => _pairs.Length;
    };
}