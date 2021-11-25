export class Node {
    public value: number;
    public pairs: Array<Node>;

    constructor(val: number) {
      this.value = val;
      this.pairs = [];
    }
    
    pairsCount() {
      return this.pairs.length;
    }
  
    add(node: Node) {
      this.pairs.push(node);
    }
  
    static compareNodes(a: Node, b: Node) {
      if (a.pairsCount() < b.pairsCount()) {
        return -1;
      }
      return 1;
    }
  }