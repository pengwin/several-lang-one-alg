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


        public Node(int val)
        {
            Value = val;
            _buildTimePairs = new List<Node>();
            _pairs = Array.Empty<Node>();
        }


        public int Value { get; }

        public Span<Node> Pairs => _pairs;

        
        public void Add(Node node)
        {
            _buildTimePairs.Add(node);
        }

        public void FinalizePairs()
        {
            _pairs = _buildTimePairs.ToArray();
        }

        public int PairsCount => _pairs.Length;
        
        public int PairsNotInPath(Path? path)
        {
            if (path == null)
            {
                return PairsCount;
            }

            var count = 0;
            for (var i = 0; i < Pairs.Length; i++)
            {
                var nn = Pairs[i];
                if (!path.Contains(nn.Value))
                {
                    count++;
                }
            }

            return count;
        }
    };
}