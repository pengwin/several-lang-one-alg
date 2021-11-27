package tests

import (
	"testing"

	"github.com/stretchr/testify/assert"
	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

func TestCustomSorting_WithoutPath_WithoutCollision(t *testing.T) {

	// arrange
	var sorting = sums.NewNodesSortingCustom(nil, 4)
	var nodes = make([]*sums.Node, 2)

	var node = sums.NewNode(3)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))
	node.Add(sums.NewNode(1))

	nodes[0] = node

	node = sums.NewNode(4)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{4, 3})
}

func TestCustomSorting_WithPath_WithoutCollision(t *testing.T) {

	// arrange
	var path = sums.NewPath(4)
	var sorting = sums.NewNodesSortingCustom(path, 4)
	var nodes = make([]*sums.Node, 2)

	path.Push(2)

	var node = sums.NewNode(3)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))
	node.Add(sums.NewNode(1))

	nodes[0] = node

	node = sums.NewNode(4)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{4, 3})
}

func TestCustomSorting_WithPath_WithCollision_onAllItems(t *testing.T) {

	// arrange
	var path = sums.NewPath(9)
	var sorting = sums.NewNodesSortingCustom(path, 9)
	var nodes = make([]*sums.Node, 3)

	path.Push(2)
	path.Push(8)
	path.Push(9)

	var node = sums.NewNode(3)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[0] = node

	node = sums.NewNode(4)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	node = sums.NewNode(7)
	node.Add(sums.NewNode(8))
	node.Add(sums.NewNode(9))
	node.Add(sums.NewNode(5))

	nodes[2] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{4, 3, 7})
}

func TestCustomSorting_WithPath_WithCollision_OnTwoItems(t *testing.T) {

	// arrange
	var path = sums.NewPath(9)
	var sorting = sums.NewNodesSortingCustom(path, 9)
	var nodes = make([]*sums.Node, 3)

	path.Push(2)
	path.Push(8)
	path.Push(9)

	var node = sums.NewNode(3)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))
	node.Add(sums.NewNode(4))

	nodes[0] = node

	node = sums.NewNode(4)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	node = sums.NewNode(7)
	node.Add(sums.NewNode(8))
	node.Add(sums.NewNode(9))
	node.Add(sums.NewNode(5))

	nodes[2] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{4, 7, 3})
}

func TestCustomSorting_WithoutPath_WitCollision(t *testing.T) {

	// arrange
	var sorting = sums.NewNodesSortingCustom(nil, 4)
	var nodes = make([]*sums.Node, 4)

	var node = sums.NewNode(3) // 3 children
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))
	node.Add(sums.NewNode(1))

	nodes[0] = node

	node = sums.NewNode(4) // 2 children
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	node = sums.NewNode(1)
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[2] = node

	node = sums.NewNode(2)
	node.Add(sums.NewNode(3))

	nodes[3] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{2, 4, 1, 3})
}

func TestCustomSorting_WithoutPath_WitCollision_SetFirst(t *testing.T) {

	// arrange
	var sorting = sums.NewNodesSortingCustom(nil, 4)
	var nodes = make([]*sums.Node, 3)

	var node = sums.NewNode(3) // 3 children
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))
	node.Add(sums.NewNode(1))

	nodes[0] = node

	node = sums.NewNode(1) // 2 children
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[1] = node

	node = sums.NewNode(4) // 2 children
	node.Add(sums.NewNode(2))
	node.Add(sums.NewNode(3))

	nodes[2] = node

	// act
	sorting.SortNodes(nodes)

	// assert
	actual := make([]int, len(nodes))
	for i, n := range nodes {
		actual[i] = n.Value()
	}

	assert.ElementsMatch(t, actual, []int{4, 1, 3})
}
