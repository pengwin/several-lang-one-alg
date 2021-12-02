package sums

import (
	"math"
)

func IsFairSquare(n int) bool {
	sqrt := math.Sqrt(float64(n))
	return sqrt-math.Floor(sqrt) == 0
}

func buildTree(n int, sorting NodesSortingFacade) *Tree {
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

	tree.SortPairsWithSorting(sorting)
	return tree
}

func dfs(n int, node *Node, path *Path, metrics *Metrics, sorting NodesSortingFacade) {

	if metrics != nil {
		metrics.IncrementDfsCounter()
	}

	sorting.SortNodes(node.Pairs())

	for _, p := range node.Pairs() {
		v := p.Value()

		if path.Contains(v) {
			continue
		}

		path.Push(v)

		if path.Count() == n {
			return
		}

		dfs(n, p, path, metrics, sorting)
		if path.Count() == n {
			return
		}

		path.Pop()
	}
}

func SquareSumsRow(n int, metrics *Metrics, sortingFactory NodesSortingFactory) []int {
	sorting := sortingFactory(nil, n)
	tree := buildTree(n, sorting)
	if tree == nil {
		return nil
	}

	for _, root := range tree.Roots() {
		path := NewPath(n)
		sorting := sortingFactory(path, n)
		path.Push(root.Value())
		dfs(n, root, path, metrics, sorting)
		if path.Count() == n {
			if metrics != nil {
				metrics.FinalizeDfsCounter(n)
			}
			return path.Array()
		}
	}
	return nil
}
