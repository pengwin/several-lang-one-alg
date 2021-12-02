using System;

// ReSharper disable ForCanBeConvertedToForeach

namespace SquareSums
{
    public class NativeNodesSorting: INodesSorting
    {
        private readonly NodesComparer _comparer;

        public NativeNodesSorting(Path? path, int maxN)
        {
            _comparer = new NodesComparer(path, maxN);
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            _comparer.FlushCache();
            
            nodes.Sort(_comparer);
        }
    }
}