package sums

import (
	"fmt"
	"strings"
)

type PathNode struct {
	prev  *PathNode
	value int
}

func NewPathNode(n int, prev *PathNode) *PathNode {
	return &PathNode{prev: prev, value: n}
}

type Path struct {
	first    *PathNode
	last     *PathNode
	count    int
	attached []bool
}

func NewPath(capacity int) *Path {

	attached := make([]bool, capacity+1)
	for i := 0; i < capacity+1; i++ {
		attached[i] = false
	}

	return &Path{
		count:    0,
		attached: attached,
	}
}

func (p *Path) Contains(n int) bool {
	return p.attached[n]
}

func (p *Path) Count() int {
	return p.count
}

func (p *Path) Push(n int) {
	p.attach(n)
}

func (p *Path) Pop() {
	if p.last == nil {
		return
	}
	p.attached[p.last.value] = false
	prev := p.last.prev
	p.last = prev
	p.count--
}

func (p *Path) attach(n int) {
	if p.attached[n] {
		panic(fmt.Sprintf("already attached %d", n))
	}

	prev := p.last
	p.last = NewPathNode(n, prev)

	if prev == nil {
		p.first = p.last
	}

	p.attached[n] = true
	p.count++
}

func (p *Path) toArray(node *PathNode, a []int) []int {
	if node == nil {
		return a
	}

	a = append(a, node.value)
	if node.prev == nil {
		return a
	}
	return p.toArray(node.prev, a)
}

func (p *Path) Array() []int {
	a := p.toArray(p.last, make([]int, 0))
	for i := len(a)/2 - 1; i >= 0; i-- {
		opp := len(a) - 1 - i
		a[i], a[opp] = a[opp], a[i]
	}
	return a
}

func (p *Path) String() string {
	source := p.Array()
	a := make([]string, len(source))
	for i, n := range source {
		a[i] = fmt.Sprintf("%d", n)
	}

	return fmt.Sprintf("[%s] %d", strings.Join(a, ","), p.count)
}
