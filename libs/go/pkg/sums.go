package sums

import (
	"math"
	"sort"
)

func IsFairSquare(n int) bool {
	sqrt := math.Sqrt(float64(n))
	return sqrt-math.Floor(sqrt) == 0
}

func buildTree(n int) *Tree {
	tree := NewTree(n)

	for i := 1; i <= n; i++ {

		for j := 1; j <= n; j++ {
			if i == j {
				continue
			}

			sum := i + j
			if !IsFairSquare(sum) {
				continue
			}
			tree.AddPair(i, j)
		}
	}

	if !tree.VerifyAllNodesHavePairs() {
		return nil
	}

	tree.SortPairs()
	return tree
}

func dfs(n int, node *Node, path *Path) {

	pairs := make([]*Node, node.PairsCount())
	copy(pairs, node.Pairs())
	sortFn := func(i, j int) bool {
		a := pairs[i].PairsNotInPathCount(path)
		b := pairs[j].PairsNotInPathCount(path)
		return a < b
	}
	sort.Slice(pairs, sortFn)

	for _, p := range pairs {
		v := p.Value()

		if path.Contains(v) {
			continue
		}

		path.Push(v)

		if path.Count() == n {
			return
		}

		dfs(n, p, path)
		if path.Count() == n {
			return
		}

		path.Pop()
	}
}

func SquareSumsRow(n int) []int {
	tree := buildTree(n)
	if tree == nil {
		return nil
	}

	for _, root := range tree.Roots() {
		path := NewPath()
		path.Push(root.Value())
		dfs(n, root, path)
		if path.Count() == n {
			return path.Array()
		}
	}
	return nil
}
