using System;

// ReSharper disable ForCanBeConvertedToForeach

namespace SquareSums
{
    public class QSortNodesSorting : INodesSorting
    {
        private readonly NodesComparer _comparer;

        public QSortNodesSorting(Path? path, int maxN)
        {
            _comparer = new NodesComparer(path, maxN);
        }

        public void SortNodes(Span<Node> nodes)
        {
            _comparer.FlushCache();

            Quicksort(_comparer, nodes, 0, nodes.Length - 1);
        }

        private static int Partition(NodesComparer comparer, Span<Node> nodes, int start, int end)
        {
            int marker = start;
            for (int i = start; i < end; i++)
            {
                var compareResult = comparer.Compare(nodes[i], nodes[end]);
                if (compareResult == -1)
                {
                    (nodes[marker], nodes[i]) = (nodes[i], nodes[marker]);
                    marker += 1;
                }
            }
            
            (nodes[marker], nodes[end]) = (nodes[end], nodes[marker]);
            return marker;
        }

        static void Quicksort(NodesComparer comparer, Span<Node> nodes, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int pivot = Partition(comparer, nodes, start, end);
            Quicksort(comparer, nodes, start, pivot - 1);
            Quicksort(comparer, nodes, pivot + 1, end);
        }
    }
}