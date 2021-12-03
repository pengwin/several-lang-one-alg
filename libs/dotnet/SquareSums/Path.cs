using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SquareSums
{
    public class Path
    {
        //private PathNode? _last;
        private int _count;
        private readonly bool[] _attached;
        private readonly int[] _nodes;

        public Path(int capacity)
        {
            _count = 0;
            _attached = new bool[capacity + 1];
            _nodes = new int[capacity + 1];
            for (var i = 0; i < capacity + 1; i++)
            {
                _attached[i] = false;
                _nodes[i] = 0;
            }
        }
        
        public bool Contains(int n)
        {
            return _attached[n];
        }

        public int Count => _count;

        public void Push(int n)
        {
            if (_attached[n])
            {
                throw new Exception("Already attached");
            }

            _nodes[_count] = n;
            
            _attached[n] = true;
            _count++;
        }

        public void Pop()
        {
            if (_count == 0)
            {
                return;
            }

            var currentIndex = _count - 1;
            var n = _nodes[currentIndex];
            _attached[n] = false;
            _nodes[_count - 1] = 0;
            _count--;
        }

        public Span<int> AsSpan()
        {
            var end = _count;
            return _nodes.AsSpan(..end);
        }
    }
}