using System;
using System.Collections.Generic;
// ReSharper disable ForCanBeConvertedToForeach

namespace SquareSums
{

    internal class NodesComparer : IComparer<Node>
    {
        private Path? _path;
        private readonly int[] _cache;

        public NodesComparer(Path? path, int maxN)
        {
            _path = path;
            if (_path != null)
            {
                _cache = new int[maxN+1];
            }
            else
            {
                _cache = Array.Empty<int>();
            }
        }
        
        public int Compare(Node? x, Node? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            
            if (_path != null)
            {
                var paisNotInPathA = GetPairsNotInPath(x);
                var paisNotInPathB = GetPairsNotInPath(y);

                if (paisNotInPathA != paisNotInPathB)
                {
                    return paisNotInPathA.CompareTo(paisNotInPathB);
                }
            }
            
            var a = x.PairsCount;
            var b = y.PairsCount;
            if (a != b)
            {
                return a.CompareTo(b);
            }

            a = x.Value;
            b = y.Value;

            if (a > b)
            {
                return -1;
            }

            return a < b ? 1 : 0;
        }
        
        public void FlushCache()
        {
            for (var index = 0; index < _cache.Length; index++)
            {
                _cache[index] = -1;
            }
        }

        private int GetPairsNotInPath(Node node)
        {
            var result = _cache[node.Value];
            if (result == -1)
            {
                result = node.PairsNotInPath(_path);
                _cache[node.Value] = result;
            }

            return result;
        }
    }

    internal class NativeNodesSorting: INodesSorting
    {
        private readonly NodesComparer _comparer;

        public NativeNodesSorting(Path? path, int maxN)
        {
            _comparer = new NodesComparer(path, maxN);
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            _comparer.FlushCache();
            
            nodes.Sort(_comparer);
        }
    }
}