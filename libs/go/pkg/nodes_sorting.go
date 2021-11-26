package sums

type SortBucketList struct {
	path *Path
	list []*Node
}

func newSortBucketList(path *Path) *SortBucketList {
	return &SortBucketList{
		path: path,
		list: make([]*Node, 0),
	}
}

func (rcvr *SortBucketList) Nodes() []*Node {
	return rcvr.list
}

func (rcvr *SortBucketList) AddNode(node *Node) {
	var found bool = false
	for index, currentNode := range rcvr.list {
		var condition bool
		if rcvr.path != nil {
			a := node.PairsCount()
			b := currentNode.PairsCount()
			if a != b {
				condition = a < b
			} else {
				condition = node.Value() > currentNode.Value()
			}
		} else {
			condition = node.Value() > currentNode.Value()
		}

		if condition {
			rcvr.list = append(rcvr.list[:index+1], rcvr.list[index:]...)
			rcvr.list[index] = node
			found = true
			break
		}
	}

	// reached end of list without adding
	if !found {
		rcvr.list = append(rcvr.list, node)
	}
}

type NodesSorting struct {
	path *Path
	maxN int
}

func NewNodesSorting(path *Path, maxN int) *NodesSorting {
	return &NodesSorting{
		path: path,
		maxN: maxN,
	}
}

func (rcvr *NodesSorting) SortNodes(nodes []*Node) []*Node {
	sortList := make([]*SortBucketList, rcvr.maxN+1)

	for _, node := range nodes {
		pairsCount := 0
		if rcvr.path == nil {
			pairsCount = node.PairsCount()
		} else {
			pairsCount = node.PairsNotInPathCount(rcvr.path)
		}

		list := sortList[pairsCount]
		if list == nil {
			list = newSortBucketList(rcvr.path)
		}

		list.AddNode(node)

		sortList[pairsCount] = list
	}

	result := make([]*Node, len(nodes))

	index := 0
	for _, list := range sortList {
		if list == nil {
			continue
		}

		for _, n := range list.Nodes() {
			result[index] = n
			index++
		}
	}
	return result
}
