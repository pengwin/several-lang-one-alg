using System.Collections.Generic;
using FluentAssertions;
using SquareSums;
using Xunit;

namespace Tests
{
    public class CalculatorTests
    {
        private  void Verify(IReadOnlyList<int> a, int n)
        {
            a.Should().HaveCount(n);

            var unique = new HashSet<int>();

            for (int i = 0; i < n-1; i++)
            {
                var x = a[i];
                unique.Should().NotContain(x, $"Duplicate number {x}");

                unique.Add(x);
                var sum = x + a[i + 1];
                Calculator<NativeNodesSorting>.IsFairSquare(sum).Should().BeTrue($"Wrong square number {sum}");
            }
        }

        [Theory]
        [InlineData(15)]
        [InlineData(23)]
        [InlineData(25)]
        [InlineData(26)]
        [InlineData(2000)]
        public void TestSums_WithCustomSorting(int n)
        {
            var actual = Calculator<CustomNodesSorting>.SquareSumsRow(n, null, NodesSortingFactory.CreateCustomSorting);
            
            Verify(actual, n);
        }
        
        [Theory]
        [InlineData(15)]
        [InlineData(23)]
        [InlineData(25)]
        [InlineData(26)]
        [InlineData(2000)]
        public void TestSums_WithNativeSorting(int n)
        {
            var actual = Calculator<NativeNodesSorting>.SquareSumsRow(n, null, NodesSortingFactory.CreateNativeSorting);
            
            Verify(actual, n);
        }
    }
}