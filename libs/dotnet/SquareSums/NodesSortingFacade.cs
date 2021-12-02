using System;

namespace SquareSums
{
    public class NodesSortingFacade: INodesSorting
    {
        private readonly QSortNodesSorting _sorting;
        
        public NodesSortingFacade(Path? path, int maxN)
        {
            _sorting = new QSortNodesSorting(path, maxN);
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            _sorting.SortNodes(nodes);
        }
    }
}