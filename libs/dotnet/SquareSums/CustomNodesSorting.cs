using System;
using System.Collections.Generic;

namespace SquareSums
{

    class SortBucketList
    {
        private readonly Path? _path;
        private readonly List<Node> _list;

        public SortBucketList(Path? path)
        {
            _path = path;
            _list = new List<Node>();
        }

        public IReadOnlyList<Node> Nodes => _list;

        public void AddNode(Node node)
        {
            bool found = false;
            for (int i = 0; i < _list.Count; i++)
            {
                var currentNode = _list[i];
                bool condition;
                if (_path != null)
                {
                    var a = node.PairsCount;
                    var b = currentNode.PairsCount;
                    if (a != b)
                    {
                        condition = a < b;
                    }
                    else
                    {
                        condition = node.Value > currentNode.Value;
                    }
                }
                else
                {
                    condition = node.Value > currentNode.Value;
                }

                if (condition)
                {
                    _list.Insert(i, node);
                    found = true;
                    break;
                }
            }

            // reached end of list without adding
            if (!found)
            {
                _list.Add(node);
            }
        }
    }
    
    public class CustomNodesSorting: INodesSorting
    {
        private readonly Path? _path;
        private readonly int _maxN;

        public CustomNodesSorting(Path? path, int maxN)
        {
            _path = path;
            _maxN = maxN;
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            var sortList = new SortBucketList?[_maxN + 1];

            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                var pairsCount = _path == null ? node.PairsCount : PairsNotInPath(node, _path);

                var list = sortList[pairsCount];
                if (list == null)
                {
                    list = new SortBucketList(_path);
                }
                
                list.AddNode(node);

                sortList[pairsCount] = list;
            }

            var targetPos = 0;
            for (var i = 0; i < sortList.Length; i++)
            {
                var list = sortList[i];
                if (list == null)
                {
                    continue;
                }

                for (var j = 0; j < list.Nodes.Count; j++)
                {
                    var node = list.Nodes[j];
                    nodes[targetPos] = node;
                    targetPos++;
                }
            }
        }
        
        private static int PairsNotInPath(Node node, Path path)
        {
            var count = 0;
            var pairs = node.Pairs;
            for (var i = 0; i < pairs.Length; i++)
            {
                if (!path.Contains(pairs[i].Value))
                {
                    count++;
                }
            }

            return count;
        }
    }
}