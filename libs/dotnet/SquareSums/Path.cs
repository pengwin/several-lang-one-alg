using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SquareSums
{
    public class Path
    {
        private PathNode? _last;
        private int _count;
        private readonly bool[] _attached;

        public Path(int capacity)
        {
            _count = 0;
            _last = null;
            _attached = new bool[capacity + 1];
            for (var i = 0; i < capacity + 1; i++)
            {
                _attached[i] = false;
            }
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            var prev = _last;
            _last = new PathNode(n, prev);
            
            _attached[n] = true;
            _count++;
        }

        public void Pop()
        {
            if (_last == null)
            {
                return;
            }

            _attached[_last.Value] = false;
            var prev = _last.Prev;
            _last = prev;
            _count--;
        }

        public IReadOnlyList<int> ToVector()
        {
            return ToVector(_last, new List<int>());
        }

        private IReadOnlyList<int> ToVector(PathNode? node, List<int> v)
        {
            while (node != null)
            {
                v.Add(node.Value);
                node = node.Prev;
            }

            return v;
        }
    }
}