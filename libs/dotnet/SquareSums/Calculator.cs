using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SquareSums
{
    public static class Calculator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFairSquare(int n)
        {
            int h = n & 0xF;  // h is the last hex "digit"
            if (h > 9)
                return false;
            // Use lazy evaluation to jump out of the if statement as soon as possible
            if (h != 2 && h != 3 && h != 5 && h != 6 && h != 7 && h != 8)
            {
                int t = (int) Math.Floor( Math.Sqrt(n));
                return t*t == n;
            }
            return false;
        }

        private static Tree? BuildTree(int n, NodesSortingFacade sorting)
        {
            var tree = new Tree(n);

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

        private static void Dfs(int n, Node node, Path path, Metrics? metrics, NodesSortingFacade sorting)
        {
            metrics?.IncrementDfsCounter();
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

                Dfs(n, p, path, metrics, sorting);
                if (path.Count == n)
                {
                    break;
                }

                path.Pop();
            }
        }

        public static IReadOnlyList<int> SquareSumsRow(int n, Metrics? metrics)
        {
            var sortingForTree = new NodesSortingFacade(null, n);
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
                var sorting = new NodesSortingFacade(path, n);
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