using System.Linq;
using FluentAssertions;
using SquareSums;
using Xunit;

namespace Tests
{
    public class SortingTests
    {
        [Fact]
        public void WithoutPath_WithoutCollision()
        {
            
            // arrange
            var sorting = new NodesSorting(null, 4);
            var nodes = new Node[2];

            var node = new Node(3);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.Add(new Node(1));
            node.FinalizePairs();

            nodes[0] = node;
            
            node = new Node(4);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[1] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(4, 3);
        }
        
        [Fact]
        public void WithPath_WithoutCollision()
        {
            
            // arrange
            var path = new Path(4);
            var sorting = new NodesSorting(path, 4);
            var nodes = new Node[2];
            
            path.Push(2);

            var node = new Node(3);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.Add(new Node(1));
            node.FinalizePairs();
            
            nodes[0] = node;
            
            node = new Node(4);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[1] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(4, 3);
        }
        
        [Fact]
        public void WithPath_WithCollision_onAllItems()
        {
            
            // arrange
            var path = new Path(9);
            var sorting = new NodesSorting(path, 9);
            var nodes = new Node[3];
            
            path.Push(2);
            path.Push(8);
            path.Push(9);

            var node = new Node(3);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[0] = node;
            
            node = new Node(4);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();
            
            nodes[1] = node;
            
            node = new Node(7);
            node.Add(new Node(8));
            node.Add(new Node(9));
            node.Add(new Node(5));
            node.FinalizePairs();

            nodes[2] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(4, 3, 7);
        }
        
        [Fact]
        public void WithPath_WithCollision_OnTwoItems()
        {
            
            // arrange
            var path = new Path(9);
            var sorting = new NodesSorting(path, 9);
            var nodes = new Node[3];
            
            path.Push(2);
            path.Push(8);
            path.Push(9);

            var node = new Node(3);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.Add(new Node(4));
            node.FinalizePairs();

            nodes[0] = node;
            
            node = new Node(4);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();
            
            nodes[1] = node;
            
            node = new Node(7);
            node.Add(new Node(8));
            node.Add(new Node(9));
            node.Add(new Node(5));
            node.FinalizePairs();

            nodes[2] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(4, 7, 3);
        }
        
        [Fact]
        public void WithoutPath_WitCollision()
        {
            
            // arrange
            var sorting = new NodesSorting(null, 4);
            var nodes = new Node[4];

            var node = new Node(3); // 3 children
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.Add(new Node(1));
            node.FinalizePairs();
            
            nodes[0] = node;
            
            node = new Node(4);  // 2 children
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[1] = node;
            
            node = new Node(1);
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[2] = node;
            
            node = new Node(2);
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[3] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(2, 4, 1, 3);
        }
        
        [Fact]
        public void WithoutPath_WitCollision_SetFirst()
        {
            
            // arrange
            var sorting = new NodesSorting(null, 4);
            var nodes = new Node[3];

            var node = new Node(3); // 3 children
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.Add(new Node(1));
            node.FinalizePairs();
            
            nodes[0] = node;
            
            node = new Node(1); // 2 children
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[1] = node;
            
            node = new Node(4);  // 2 children
            node.Add(new Node(2));
            node.Add(new Node(3));
            node.FinalizePairs();

            nodes[2] = node;
            
            // act
            sorting.SortNodes(nodes);
            
            // assert
            nodes.Select(n => n.Value).Should().Equal(4, 1, 3);
        }
    }
}

