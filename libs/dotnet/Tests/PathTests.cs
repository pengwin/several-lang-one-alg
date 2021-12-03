using FluentAssertions;
using SquareSums;
using Xunit;

namespace Tests
{
    public class PathTests
    {
        [Fact]
        public void Path_Push()
        {
            // arrange
            var path = new Path(10);
            
            // act
            path.Push(1);
            path.Push(8);

            //assert
            path.Contains(1).Should().BeTrue();
            path.Contains(8).Should().BeTrue();
            var nodes = path.AsSpan().ToArray();
            nodes.Should().HaveCount(2);
            nodes.Should().Equal(1, 8);
        }
        
        [Fact]
        public void Path_Pop()
        {
            // arrange
            var path = new Path(10);
            
            // act
            path.Push(1);
            path.Push(8);
            path.Push(3);
            path.Pop();

            //assert
            path.Contains(1).Should().BeTrue();
            path.Contains(8).Should().BeTrue();
            path.Contains(3).Should().BeFalse();
            var nodes = path.AsSpan().ToArray();
            nodes.Should().HaveCount(2);
            nodes.Should().Equal(1, 8);
        }
    }
}