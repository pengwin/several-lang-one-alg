package sums

import (
	"fmt"
	"sort"
	"strings"
)

type Node struct {
	value int
	pairs []*Node
}

func NewNode(n int) *Node {
	return &Node{
		value: n,
		pairs: make([]*Node, 0),
	}
}

func sortNodesFunc(nodes []*Node) func(int, int) bool {
	return func(i, j int) bool {
		pairsCountA := nodes[i].PairsCount()
		pairsCountB := nodes[j].PairsCount()
		return pairsCountA < pairsCountB
	}
}

func (n *Node) Value() int {
	return n.value
}

func (n *Node) Pairs() []*Node {
	return n.pairs
}

func (n *Node) SortPairs() {
	sort.Slice(n.pairs, sortNodesFunc(n.pairs))
}

func (n *Node) String() string {
	pairs := make([]string, len(n.pairs))
	for i, p := range n.pairs {
		pairs[i] = fmt.Sprintf("%d", p.value)
	}
	return fmt.Sprintf("%d: [%s]", n.value, strings.Join(pairs, ", "))
}

func (n *Node) Add(node *Node) {
	n.pairs = append(n.pairs, node)
}

func (n *Node) PairsCount() int {
	return len(n.pairs)
}

func (n *Node) PairsNotInPathCount(path *Path) int {
	count := 0
	for _, p := range n.pairs {
		if path.Contains(p.Value()) {
			continue
		}
		count++
	}
	return count
}

func (n *Node) PrintPairs() {
	fmt.Printf("%d:[", n.Value())
	for _, p := range n.pairs {
		fmt.Printf("%d, ", p.Value())
	}
	fmt.Printf("]\n")
}

type Tree struct {
	n     int
	nodes []*Node
}

func NewTree(n int) *Tree {
	return &Tree{
		n:     n,
		nodes: make([]*Node, n),
	}
}

func (t *Tree) Roots() []*Node {
	return t.nodes
}

func (t *Tree) getOrCreate(n int) *Node {
	node := t.nodes[n-1]
	if node == nil {
		node = NewNode(n)
		t.nodes[n-1] = node
	}
	return node
}

func (t *Tree) AddPair(head, tail int) {
	headNode := t.getOrCreate(head)
	tailNode := t.getOrCreate(tail)
	headNode.Add(tailNode)
}

func (t *Tree) VerifyAllNodesHavePairs() bool {
	for _, n := range t.nodes {
		if n == nil {
			return false
		}
		if len(n.pairs) == 0 {
			return false
		}
	}
	return true
}

func (t *Tree) SortPairs() {
	for _, n := range t.nodes {
		n.SortPairs()
	}
	sort.Slice(t.nodes, sortNodesFunc(t.nodes))
}

func (t *Tree) String() string {
	a := make([]string, len(t.nodes))
	for i, n := range t.nodes {
		a[i] = n.String()
	}
	return strings.Join(a, "\n")
}
