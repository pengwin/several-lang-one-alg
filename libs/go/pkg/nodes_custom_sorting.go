package sums

//import "sort"

type SortBucketList struct {
	path *Path
	list []*Node
}

func newSortBucketList(path *Path, capacity int) *SortBucketList {
	return &SortBucketList{
		path: path,
		list: make([]*Node, 0, capacity),
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

type NodesSortingCustom struct {
	path *Path
	maxN int
}

func NewNodesSortingCustom(path *Path, maxN int) *NodesSortingCustom {
	return &NodesSortingCustom{
		path: path,
		maxN: maxN,
	}
}

func (rcvr *NodesSortingCustom) SortNodes(nodes []*Node) {

	/*sortFunc := func(i int, j int) bool {

		nodeA := (*nodes)[i]
		nodeB := (*nodes)[j]

		var condition bool
		if rcvr.path != nil {
			a := nodeA.PairsCount()
			b := nodeB.PairsCount()
			if a != b {
				condition = a < b
			} else {
				condition = nodeA.Value() > nodeB.Value()
			}
		} else {
			condition = nodeA.Value() > nodeB.Value()
		}
		return condition
	}

	sort.Slice(*nodes, sortFunc)*/

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
			list = newSortBucketList(rcvr.path, rcvr.maxN/2)
		}

		list.AddNode(node)

		sortList[pairsCount] = list
	}

	index := 0
	for _, list := range sortList {
		if list == nil {
			continue
		}

		for _, n := range list.Nodes() {
			nodes[index] = n
			index++
		}
	}
}
