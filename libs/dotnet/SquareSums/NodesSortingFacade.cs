using System;

namespace SquareSums
{
    public class NodesSortingFacade: INodesSorting
    {
        private readonly NativeNodesSorting _sorting;
        
        public NodesSortingFacade(Path? path, int maxN)
        {
            _sorting = new NativeNodesSorting(path, maxN);
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            _sorting.SortNodes(nodes);
        }
    }
}