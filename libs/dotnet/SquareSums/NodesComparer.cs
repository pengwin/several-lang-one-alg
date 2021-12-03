using System;
using System.Collections.Generic;

namespace SquareSums
{
    internal class NodesComparer : IComparer<Node>
    {
        private Path? _path;
        private readonly int[] _cache;
        private int _maxCachePosition = 0;

        public NodesComparer(Path? path, int maxN)
        {
            _path = path;
            if (_path != null)
            {
                _cache = new int[maxN+1];
                ClearCache(maxN);
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
            if (_cache.Length == 0)
            {
                return;
            }

            if (_maxCachePosition == 0)
            {
                return;
            }
            
            ClearCache(_maxCachePosition);
            _maxCachePosition = 0;
        }

        private void ClearCache(int toIndex)
        {
            Array.Fill(_cache, -1, 0, toIndex+1);
        }

        private int GetPairsNotInPath(Node node)
        {
            var result = _cache[node.Value];
            if (result != -1)
            {
                return result;
            }

            var pairValues = node.PairsValues;
            result = _path!.PairsNotInPath(pairValues);
            _cache[node.Value] = result;
            _maxCachePosition = Math.Max(node.Value, _maxCachePosition);
            return result;
        }
    }
}