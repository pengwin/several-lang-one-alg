using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SquareSums
{
    public static class Calculator
    {
        public delegate INodesSorting NodesSortingFactory(Path? path, int maxN);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFairSquare(int n)
        {
            double sqrtVal = Math.Sqrt(n);
            return sqrtVal - Math.Floor(sqrtVal) == 0;
        }

        private static Tree? BuildTree(int n, INodesSorting sorting)
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

            if (!tree.FinalizePairsAndVerifyAllNodesHavePairs())
            {
                return null;
            }
            
            tree.SortPairsUsing(sorting);
            return tree;
        }

        private static void Dfs(int n, Node node, Path path, Metrics? metrics, INodesSorting nodesSorting)
        {
            metrics?.IncrementDfsCounter();
            var sorting = new CustomNodesSorting(path, n);
            sorting.SortNodes(node.Pairs);

            for (var i = 0; i < node.Pairs.Length; i++)
            {
                var p = node.Pairs[i];
                int v = p.Value;

                if (path.Contains(v))
                {
                    continue;
                }

                path.Push(v);

                if (path.Count == n)
                {
                    break;
                }

                Dfs(n, p, path, metrics, nodesSorting);
                if (path.Count == n)
                {
                    break;
                }

                path.Pop();
            }
        }

        public static IReadOnlyList<int> SquareSumsRow(int n, Metrics? metrics, NodesSortingFactory sortingFactory)
        {
            var sortingForTree = sortingFactory(null, n);
            var tree = BuildTree(n, sortingForTree);
            if (tree == null)
            {
                return Array.Empty<int>();
            }

            var list = tree.Roots();
            for (var i = 0; i < list.Count; i++)
            {
                var root = list[i];
                var path = new Path(n);
                var sorting = sortingFactory(path, n);
                if (root == null)
                {
                    throw new NullReferenceException("Unexpected null");
                }

                path.Push(root.Value);
                Dfs(n, root, path, metrics, sorting);
                if (path.Count == n)
                {
                    IReadOnlyList<int> result = path.ToVector();
                    metrics?.FinalizeDfsCounter(n);
                    return result;
                }
            }

            return Array.Empty<int>();
        }
    }
}