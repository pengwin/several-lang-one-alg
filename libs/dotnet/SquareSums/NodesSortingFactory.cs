namespace SquareSums
{
    public static class NodesSortingFactory
    {
        public static NodesSorting<CustomNodesSorting> CreateCustomSorting(Path? path, int maxN) => new (new CustomNodesSorting(path, maxN));
        
        public static NodesSorting<NativeNodesSorting> CreateNativeSorting(Path? path, int maxN) => new (new NativeNodesSorting(path, maxN));
    }
}