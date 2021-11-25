import { Node } from './Node';
import { NodesSorting } from './NodesSorting';

export class Tree {
    public nodes: Array<Node>;
    constructor(n: number) {
      this.nodes = Array(n).fill(null);
    }
  
    getOrCreate(n: number) {
      let node = this.nodes[n - 1];
      if (!node) {
        node = new Node(n);
        this.nodes[n - 1] = node;
      }
      return node;
    }
  
    addPair(head: number, tail: number) {
      let headNode = this.getOrCreate(head);
      let tailNode = this.getOrCreate(tail);
      headNode.add(tailNode);
    }
  
    verifyAllNodesHavePairs() {
      for (var n of this.nodes) {
        if (!n) {
          return false;
        }
  
        if (n.pairsCount() == 0) {
          return false;
        }
      }
  
      return true;
    }
  
    sortPairsWithSorting(sorting: NodesSorting) {
      for (let n of this.nodes) {
        n.pairs = sorting.sortNodes(n.pairs);
      }
  
      this.nodes = sorting.sortNodes(this.nodes);
    }
  }