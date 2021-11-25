package sums

type SortBucket struct {
	Value *Node

	Next *SortBucket
}

func newSortBucket(value *Node) *SortBucket {
	return &SortBucket{
		Value: value,
		Next:  nil,
	}
}

type SortBucketList struct {
	path  *Path
	First *SortBucket
}

func newSortBucketList(path *Path) *SortBucketList {
	return &SortBucketList{
		path:  path,
		First: nil,
	}
}

func (rcvr *SortBucketList) AddNode(node *Node) {
	if rcvr.First == nil {
		rcvr.First = newSortBucket(node)
		return
	}

	current := rcvr.First
	var prev *SortBucket = nil
	for current != nil {
		var condition bool = false
		if rcvr.path != nil {
			a := node.PairsCount()
			b := current.Value.PairsCount()
			if a != b {
				condition = a < b
			} else {
				condition = node.Value() > current.Value.Value()
			}
		} else {
			condition = node.Value() > current.Value.Value()
		}

		if condition {
			next := current
			current = newSortBucket(node)
			current.Next = next

			if prev != nil {
				prev.Next = current
			} else {
				rcvr.First = current
			}
			break
		}

		prev = current
		current = current.Next
	}

	// reached end of list without adding
	if current == nil && prev != nil {
		prev.Next = newSortBucket(node)
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

		current := list.First
		for current != nil {
			result[index] = current.Value
			index++
			current = current.Next
		}
	}
	return result
}
