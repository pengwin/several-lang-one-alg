using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{

    class SortBucketList
    {
        private readonly Path? _path;
        private List<Node> _list;

        public SortBucketList(Path? path)
        {
            _path = path;
            _list = new List<Node>();
        }

        public IEnumerable<Node> Nodes() => _list;

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
    
    internal class NodesSorting
    {
        private readonly Path? _path;
        private readonly int _maxN;

        public NodesSorting(Path? path, int maxN)
        {
            _path = path;
            _maxN = maxN;
        }
        
        public IEnumerable<Node> SortNodes(IReadOnlyList<Node> nodes)
        {
            var sortList = new SortBucketList?[_maxN + 1];

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var pairsCount = _path == null ? node.PairsCount : PairsNotInPath(node);

                var list = sortList[pairsCount];
                if (list == null)
                {
                    list = new SortBucketList(_path);
                }
                
                list.AddNode(node);

                sortList[pairsCount] = list;
            }

            for (var index = 0; index < sortList.Length; index++)
            {
                var list = sortList[index];
                if (list == null)
                {
                    continue;
                }

                foreach (var node in list.Nodes())
                {
                    yield return node;
                }
            }
        }
        
        private int PairsNotInPath(Node n)
        {
            if (_path == null)
            {
                return n.PairsCount;
            }
            return n.Pairs.Count(nn => !_path.Contains(nn.Value));
        }
    }
}