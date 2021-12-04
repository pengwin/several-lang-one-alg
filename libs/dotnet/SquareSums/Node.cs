using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable ForCanBeConvertedToForeach

namespace SquareSums
{
    public sealed class Node
    {
        /// <summary>
        /// Used while constructing tree
        /// </summary>
        private readonly List<Node> _buildTimePairs;
        /// <summary>
        /// Used while constructing tree
        /// </summary>
        private readonly List<int> _buildTimeValues;
        private Node[] _pairs;
        private int[] _values;
        
        public Node(int val)
        {
            Value = val;
            _buildTimePairs = new List<Node>(val);
            _buildTimeValues = new List<int>(val);
            _pairs = Array.Empty<Node>();
            _values = Array.Empty<int>();
        }


        public int Value { get; }

        public Span<int> PairsValues => _values.AsSpan();
        
        public Span<Node> Pairs => _pairs.AsSpan();

        
        public void Add(Node node)
        {
            _buildTimePairs.Add(node);
            _buildTimeValues.Add(node.Value);
        }

        public void FinalizePairs()
        {
            _pairs = _buildTimePairs.ToArray();
            _values = _buildTimeValues.ToArray();
            _buildTimePairs.Clear();
            _buildTimeValues.Clear();
        }

        public int PairsCount => _pairs.Length;
        
    };
}