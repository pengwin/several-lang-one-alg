using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{
    internal class NodesComparer : Comparer<Node>
    {
        private readonly Path _path;

        public NodesComparer(Path p)
        {
            _path = p;
        }

        public override int Compare(Node? x, Node? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }

            var a = PairsNotInPath(x, _path);
            var b = PairsNotInPath(y, _path);
            if (a != b)
            {
                return a.CompareTo(b);
            }

            a = x.PairsCount();
            b = y.PairsCount();
            
            if (a != b)
            {
                return a.CompareTo(b);
            }

            a = x.Value();
            b = y.Value();
            return b.CompareTo(a);
        }

        private static int PairsNotInPath(Node n, Path path)
        {
            return n.Pairs().Count(nn => !path.Contains(nn.Value()));
        }
    };
}