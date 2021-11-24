namespace SquareSums
{
    internal class PathNode
    {
        private readonly PathNode? _prev;
        private readonly int _value;

        public PathNode(int n, PathNode? prevNode)
        {
            _value = n;
            _prev = prevNode;
        }

        public int Value()
        {
            return _value;
        }

        public PathNode? Prev()
        {
            return _prev;
        }
    };
}