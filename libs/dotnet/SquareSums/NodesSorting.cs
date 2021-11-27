namespace SquareSums
{
    public static class NodesSorting
    {
        public static INodesSorting CreateCustomSorting(Path? path, int maxN) => new CustomNodesSorting(path, maxN);
        
        public static INodesSorting CreateNativeSorting(Path? path, int maxN) => new NativeNodesSorting(path, maxN);
    }
}