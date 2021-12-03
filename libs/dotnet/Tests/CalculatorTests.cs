using System;
using System.Collections.Generic;
using FluentAssertions;
using SquareSums;
using Xunit;

namespace Tests
{
    public class CalculatorTests
    {
        private  void Verify(Span<int> a, int n)
        {
            a.Length.Should().Be(n);

            var unique = new HashSet<int>();

            for (int i = 0; i < n-1; i++)
            {
                var x = a[i];
                unique.Should().NotContain(x, $"Duplicate number {x}");

                unique.Add(x);
                var sum = x + a[i + 1];
                Calculator.IsFairSquare(sum).Should().BeTrue($"Wrong square number {sum}");
            }
        }
        
        [Theory]
        [InlineData(15)]
        [InlineData(23)]
        [InlineData(25)]
        [InlineData(26)]
        [InlineData(2000)]
        public void TestSums(int n)
        {
            var actual = Calculator.SquareSumsRow(n, null);
            
            Verify(actual, n);
        }
        
        [Fact]
        public void TestSums_LongestSearch()
        {
            var n = 102;
            var actual = Calculator.SquareSumsRow(n, null);
            
            Verify(actual, n);
        }
    }
}