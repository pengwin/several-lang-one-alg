using System;
using System.Collections.Generic;
using System.Linq;

namespace SquareSums
{
    public static class Calculator
    {
        private static int _dfsCounter;

        private static bool IsFairSquare(int n)
        {
            double sqrtVal = Math.Sqrt(n);
            return sqrtVal - Math.Floor(sqrtVal) == 0;
        }

        private static Tree? BuildTree(int n)
        {
            Tree tree = new Tree(n);

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    int sum = i + j;
                    if (!IsFairSquare(sum))
                    {
                        continue;
                    }

                    tree.AddPair(i, j);
                }
            }

            if (!tree.VerifyAllNodesHavePairs())
            {
                return null;
            }
            
            tree.SortPairsUsing(new NodesSorting(null, n));
            return tree;
        }

        private static void Dfs(int n, Node node, Path path)
        {
            _dfsCounter++;
            var sorting = new NodesSorting(path, n);
            var pairs = sorting.SortNodes(node.Pairs()).ToArray();

            foreach (var p in pairs)
            {
                int v = p.Value();

                if (path.Contains(v))
                {
                    continue;
                }

                path.Push(v);

                if (path.Count() == n)
                {
                    break;
                }

                Dfs(n, p, path);
                if (path.Count() == n)
                {
                    break;
                }

                path.Pop();
            }
        }

        public static IReadOnlyList<int> SquareSumsRow(int n)
        {
            var tree = BuildTree(n);
            if (tree == null)
            {
                return Array.Empty<int>();
            }

            foreach (var root in tree.Roots())
            {
                var path = new Path(n);
                if (root == null)
                {
                    throw new NullReferenceException("Unexpected null");
                }

                path.Push(root.Value());
                Dfs(n, root, path);
                if (path.Count() == n)
                {
                    IReadOnlyList<int> result = path.ToVector();
                    if (_dfsCounter / n > 3)
                    {
                        Console.WriteLine("Counter for {0}: {1}", n, _dfsCounter);
                    }

                    _dfsCounter = 0;
                    return result;
                }
            }

            return Array.Empty<int>();
        }
    }
}