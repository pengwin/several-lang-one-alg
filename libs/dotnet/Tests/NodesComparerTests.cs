using FluentAssertions;
using FluentAssertions.Equivalency;
using SquareSums;
using Xunit;
using Node = SquareSums.Node;

namespace Tests
{
    public class NodesComparerTests
    {
        [Fact]
        public void WithoutPath_Greater()
        {
            // arrange
            var a = new Node(3); // 3 children
            a.Add(new Node(2));
            a.Add(new Node(3));
            a.Add(new Node(1));
            a.FinalizePairs();
            
            var b = new Node(3); // 2 children
            a.Add(new Node(2));
            a.Add(new Node(3));
            a.Add(new Node(1));
            a.FinalizePairs();

            var comparer = new NodesComparer(null, 3);
            
            // act
            var actual = comparer.Compare(a, b);
            
            // assert
            actual.Should().Be(1);
        }
        
        [Fact]
        public void WithoutPath_Less()
        {
            // arrange
            var a = new Node(3); // 3 children
            a.Add(new Node(2));
            a.Add(new Node(3));
            a.Add(new Node(1));
            a.FinalizePairs();
            
            var b = new Node(3); // 2 children
            a.Add(new Node(2));
            a.Add(new Node(3));
            a.Add(new Node(1));
            a.FinalizePairs();

            var comparer = new NodesComparer(null, 3);
            
            // act
            var actual = comparer.Compare(b, a);
            
            // assert
            actual.Should().Be(-1);
        }
        
        [Fact]
        public void WithoutPath_Equal()
        {
            // arrange
            var a = new Node(3); // 3 children
            a.Add(new Node(2));
            a.Add(new Node(3));
            a.Add(new Node(1));
            a.FinalizePairs();

            var comparer = new NodesComparer(null, 3);
            
            // act
            var actual = comparer.Compare(a, a);
            
            // assert
            actual.Should().Be(0);
        }
    }
}