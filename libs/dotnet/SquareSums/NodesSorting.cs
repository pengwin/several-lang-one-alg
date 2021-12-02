using System;

namespace SquareSums
{
    /// <summary>
    /// NodesSorting facade
    /// </summary>
    public class NodesSorting<T> where T: INodesSorting
    {
        private readonly T _sorting;

        public NodesSorting(T sorting)
        {
            _sorting = sorting;
        }
        
        public void SortNodes(Span<Node> nodes)
        {
            _sorting.SortNodes(nodes);
        }
    }
}