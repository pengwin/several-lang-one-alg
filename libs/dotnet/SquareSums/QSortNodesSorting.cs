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

            Quicksort(_comparer, ref nodes, 0, nodes.Length - 1);
        }

        private static int Partition(NodesComparer comparer, ref Span<Node> nodes, int start, int end)
        {
            Node temp;
            int marker = start;
            for (int i = start; i < end; i++)
            {
                var compareResult = comparer.Compare(nodes[i], nodes[end]);
                if (compareResult == -1)
                {
                    temp = nodes[marker]; 
                    nodes[marker] = nodes[i];
                    nodes[i] = temp;
                    marker += 1;
                }
            }
            
            temp = nodes[marker];
            nodes[marker] = nodes[end];
            nodes[end] = temp; 
            return marker;
        }

        static void Quicksort(NodesComparer comparer, ref Span<Node> nodes, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int pivot = Partition(comparer, ref nodes, start, end);
            Quicksort(comparer, ref nodes, start, pivot - 1);
            Quicksort(comparer, ref nodes, pivot + 1, end);
        }
    }
}