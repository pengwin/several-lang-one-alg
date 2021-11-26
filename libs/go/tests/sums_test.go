package tests

import (
	"fmt"
	"testing"

	"github.com/stretchr/testify/require"

	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

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
	t.Skip()
	valid := []int{
		15,
		23,
		25,
		26,
		2000,
	}

	for _, n := range valid {
		t.Run(fmt.Sprintf("%d", n), func(t *testing.T) {
			a := sums.SquareSumsRow(n, nil)
			verify(t, n, a)
		})
	}
}

func TestSumsAll(t *testing.T) {
	for i := 2; i <= 2000; i++ {
		t.Run(fmt.Sprintf("%d", i), func(t *testing.T) {
			a := sums.SquareSumsRow(i, nil)
			verify(t, i, a)
		})
	}
}

func BenchmarkSums(b *testing.B) {
	for i := 2; i <= 1000; i++ {
		b.Run(fmt.Sprintf("%d", i), func(b *testing.B) {
			a := sums.SquareSumsRow(i, nil)
			verify(b, i, a)
		})
	}
}
