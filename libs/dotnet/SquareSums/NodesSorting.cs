using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{

    class SortBucketList
    {
        private readonly Path? _path;
        public SortBucket? First { get; private set; }

        public SortBucketList(Path? path)
        {
            _path = path;
        }

        public void AddNode(Node node)
        {
            if (First == null)
            {
                First = new SortBucket(node);
                return;
            }

            var current = First;
            SortBucket? prev = null;
            while (current != null)
            {
                bool condition;
                if (_path != null)
                {
                    var a = node.PairsCount();
                    var b = current.Value.PairsCount();
                    if (a != b)
                    {
                        condition = a < b;
                    }
                    else
                    {
                        condition = node.Value() > current.Value.Value();
                    }
                }
                else
                {
                    condition = node.Value() > current.Value.Value();
                }

                if (condition)
                {
                    var next = current;
                    current = new SortBucket(node)
                    {
                        Next = next
                    };
                    if (prev != null)
                    {
                        prev.Next = current;
                    }
                    else
                    {
                        First = current;
                    }
                    break;
                }

                prev = current;
                current = current.Next;
            }

            // reached end of list without adding
            if (current == null && prev != null)
            {
                prev.Next = new SortBucket(node);
            }
        }
    }

    class SortBucket
    {
        public Node Value { get; }
        
        public SortBucket? Next { get; set; }

        public SortBucket(Node value)
        {
            Value = value;
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
                var pairsCount = _path == null ? node.PairsCount() : PairsNotInPath(node);

                var list = sortList[pairsCount];
                if (list == null)
                {
                    list = new SortBucketList(_path);
                }
                
                list.AddNode(node);

                sortList[pairsCount] = list;
            }

            foreach (var list in sortList)
            {
                if (list == null)
                {
                    continue;
                }

                var current = list.First;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }
        }
        
        private int PairsNotInPath(Node n)
        {
            if (_path == null)
            {
                return n.PairsCount();
            }
            return n.Pairs().Count(nn => !_path.Contains(nn.Value()));
        }
    }
}