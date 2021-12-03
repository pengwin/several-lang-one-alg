package sums

type nodesComparer struct {
	pairsNotInPathCache []int
	path                *Path
}

func newNodesComparer(path *Path, maxN int) *nodesComparer {
	return &nodesComparer{
		path:                path,
		pairsNotInPathCache: make([]int, maxN+1),
	}
}

func (rcvr *nodesComparer) FlushCache() {
	if rcvr.path == nil {
		return
	}
	for i, _ := range rcvr.pairsNotInPathCache {
		rcvr.pairsNotInPathCache[i] = -1
	}
}

func (rcvr *nodesComparer) getPairsNotInPath(node *Node) int {
	result := rcvr.pairsNotInPathCache[node.value]
	if result == -1 {
		result = node.PairsNotInPathCount(rcvr.path)
		rcvr.pairsNotInPathCache[node.value] = result
	}
	return result
}

func (rcvr *nodesComparer) Compare(nodeA *Node, nodeB *Node) int {
	if rcvr.path != nil {
		a := rcvr.getPairsNotInPath(nodeA)
		b := rcvr.getPairsNotInPath(nodeB)

		if a != b {
			if a < b {
				return -1
			}
			if a > b {
				return 1
			}
		}
	}

	a := nodeA.PairsCount()
	b := nodeB.PairsCount()

	if a != b {
		if a < b {
			return -1
		}
		if a > b {
			return 1
		}
	}

	a = nodeA.Value()
	b = nodeB.Value()

	if a > b {
		return -1
	}
	if a < b {
		return 1
	}
	return 0
}

func partition(comparer *nodesComparer, nodes []*Node, start int, end int) int {
	var temp *Node
	marker := start
	for i := start; i < end; i++ {
		var compareResult = comparer.Compare(nodes[i], nodes[end])
		if compareResult == -1 {
			temp = nodes[marker]
			nodes[marker] = nodes[i]
			nodes[i] = temp
			marker += 1
		}
	}

	temp = nodes[marker]
	nodes[marker] = nodes[end]
	nodes[end] = temp
	return marker
}

func quicksort(comparer *nodesComparer, nodes []*Node, start int, end int) {
	if start >= end {
		return
	}

	pivot := partition(comparer, nodes, start, end)
	quicksort(comparer, nodes, start, pivot-1)
	quicksort(comparer, nodes, pivot+1, end)
}

type NodesSortingQSort struct {
	comparer *nodesComparer
}

func NewNodesSortingQSort(path *Path, maxN int) *NodesSortingQSort {
	return &NodesSortingQSort{
		comparer: newNodesComparer(path, maxN),
	}
}

func (rcvr *NodesSortingQSort) SortNodes(nodes []*Node) {
	rcvr.comparer.FlushCache()
	quicksort(rcvr.comparer, nodes, 0, len(nodes)-1)
}
