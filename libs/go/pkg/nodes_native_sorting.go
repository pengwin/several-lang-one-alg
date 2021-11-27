package sums

import "sort"

type nodesSortingWrapper struct {
	pairsNotInPathCache []int
	path                *Path
	nodes               []*Node
}

func newNodesSortingWrapper(path *Path, maxN int) *nodesSortingWrapper {
	return &nodesSortingWrapper{
		path:                path,
		pairsNotInPathCache: make([]int, maxN+1),
		nodes:               []*Node{},
	}
}

func (rcvr *nodesSortingWrapper) flushPairsNotInPathCache() {
	if rcvr.path == nil {
		return
	}
	for i, _ := range rcvr.pairsNotInPathCache {
		rcvr.pairsNotInPathCache[i] = -1
	}
}

func (rcvr *nodesSortingWrapper) setNodes(nodes []*Node) {
	rcvr.flushPairsNotInPathCache()
	rcvr.nodes = nodes
}

func (rcvr *nodesSortingWrapper) getPairsNotInPath(node *Node) int {
	result := rcvr.pairsNotInPathCache[node.value]
	if result == -1 {
		result = node.PairsNotInPathCount(rcvr.path)
		rcvr.pairsNotInPathCache[node.value] = result
	}
	return result
}

func (rcvr *nodesSortingWrapper) Len() int {
	return len(rcvr.nodes)
}

func (rcvr *nodesSortingWrapper) Swap(i, j int) {
	temp := rcvr.nodes[i]
	rcvr.nodes[i] = rcvr.nodes[j]
	rcvr.nodes[j] = temp
}

func (rcvr *nodesSortingWrapper) Less(i, j int) bool {
	nodeA := rcvr.nodes[i]
	nodeB := rcvr.nodes[j]

	if rcvr.path != nil {
		a := rcvr.getPairsNotInPath(nodeA)
		b := rcvr.getPairsNotInPath(nodeB)

		if a != b {
			return a < b
		}
	}

	a := nodeA.PairsCount()
	b := nodeB.PairsCount()

	if a != b {
		return a < b
	}

	a = nodeA.Value()
	b = nodeB.Value()

	return a > b
}

type NodesSortingNative struct {
	sortingWrapper *nodesSortingWrapper
}

func NewNodesSortingNative(path *Path, maxN int) *NodesSortingNative {
	return &NodesSortingNative{
		sortingWrapper: newNodesSortingWrapper(path, maxN),
	}
}

func (rcvr *NodesSortingNative) SortNodes(nodes []*Node) {
	rcvr.sortingWrapper.setNodes(nodes)
	sort.Sort(rcvr.sortingWrapper)
}
