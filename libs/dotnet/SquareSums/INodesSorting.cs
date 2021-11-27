using System;

namespace SquareSums
{
    public interface INodesSorting
    {
        void SortNodes(Span<Node> nodes);
    }
}