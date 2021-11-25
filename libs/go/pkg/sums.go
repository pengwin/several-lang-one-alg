package sums

import (
	"fmt"
	"math"
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

	sorting := NewNodesSorting(nil, n)
	tree.SortPairsWithSorting(sorting)
	return tree
}

var dfsCount int = 0

func dfs(n int, node *Node, path *Path) {

	dfsCount++
	sorting := NewNodesSorting(path, n)

	pairs := sorting.SortNodes(node.Pairs())

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
		path := NewPath(n)
		path.Push(root.Value())
		dfs(n, root, path)
		if path.Count() == n {
			if dfsCount/n > 3 {
				fmt.Printf("Counter for %d: %d\n", n, dfsCount)
			}
			dfsCount = 0
			return path.Array()
		}
	}
	return nil
}