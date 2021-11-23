import { Node } from './Node';

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
  
    sortPairs() {
      for (let n of this.nodes) {
        n.sortPairs();
      }
  
      this.nodes = this.nodes.sort((a, b) => Node.compareNodes(a, b));
    }
  }