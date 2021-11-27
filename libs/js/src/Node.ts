import { Path } from './Path';

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
  
    pairsNotInPath(path: Path): number {
      let count = 0;
      for (let n of this.pairs) {
          if (path.contains(n.value)) {
              continue;
          }
          count++;
      }
      return count;
  }
  }