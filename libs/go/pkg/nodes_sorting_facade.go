package sums

type NodesSortingFacade struct {
	sorting *NodesSortingNative
}

func NewNodesSortingFacade(path *Path, maxN int) *NodesSortingFacade {
	return &NodesSortingFacade{
		sorting: NewNodesSortingNative(path, maxN),
	}
}

func (rcvr *NodesSortingFacade) SortNodes(nodes []*Node) {
	rcvr.sorting.SortNodes(nodes)
}

type NodesSortingFactory = func(path *Path, maxN int) NodesSortingFacade
