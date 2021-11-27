package sums

type NodesSorting interface {
	SortNodes(nodes []*Node)
}

type NodesSortingFactory = func(path *Path, maxN int) NodesSorting
