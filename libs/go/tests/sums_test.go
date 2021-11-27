package tests

import (
	"fmt"
	"testing"

	"github.com/stretchr/testify/require"

	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

func sortingFactory(path *sums.Path, maxN int) sums.NodesSorting {
	//return sums.NewNodesSortingCustom(path, maxN)
	return sums.NewNodesSortingNative(path, maxN)
}

func verify(t require.TestingT, n int, a []int) {
	if len(a) != n {
		t.Errorf("Wrong length %d expected %d", len(a), n)
		t.FailNow()
	}

	unique := make(map[int]bool)

	for i := 0; i < n-1; i++ {
		x := a[i]
		if unique[x] {
			t.Errorf("Duplicated number %d in %v", x, a)
			t.FailNow()
		}
		unique[x] = true
		sum := x + a[i+1]
		if !sums.IsFairSquare(sum) {
			t.Errorf("Wrong square number %d in %v", sum, a)
			t.FailNow()
		}
	}
}

func TestSums(t *testing.T) {
	valid := []int{
		15,
		23,
		25,
		26,
		2000,
	}

	for _, n := range valid {
		t.Run(fmt.Sprintf("%d", n), func(t *testing.T) {
			a := sums.SquareSumsRow(n, nil, sortingFactory)
			verify(t, n, a)
		})
	}
}

func BenchmarkSums(b *testing.B) {
	for i := 100; i <= 500; i++ {
		b.Run(fmt.Sprintf("%d", i), func(b *testing.B) {
			a := sums.SquareSumsRow(i, nil, sortingFactory)
			verify(b, i, a)
		})
	}
}

func BenchmarkSimpleCycle(b *testing.B) {
	for i := 1900; i <= 2000; i++ {
		a := sums.SquareSumsRow(i, nil, sortingFactory)
		verify(b, i, a)
	}
}

func BenchmarkSimpleOneShot(b *testing.B) {
	n := 102
	a := sums.SquareSumsRow(n, nil, sortingFactory)
	verify(b, n, a)
}
