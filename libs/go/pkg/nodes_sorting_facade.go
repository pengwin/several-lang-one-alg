package sums

type NodesSortingFacade struct {
	sorting *NodesSortingQSort
}

func NewNodesSortingFacade(path *Path, maxN int) *NodesSortingFacade {
	return &NodesSortingFacade{
		sorting: NewNodesSortingQSort(path, maxN),
	}
}

func (rcvr *NodesSortingFacade) SortNodes(nodes []*Node) {
	rcvr.sorting.SortNodes(nodes)
}

type NodesSortingFactory = func(path *Path, maxN int) NodesSortingFacade
