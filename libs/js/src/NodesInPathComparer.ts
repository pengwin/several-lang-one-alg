import { Node } from './Node';
import { Path } from './Path';

export class NodesInPathComparer {
    private path: Path;

    private pairsNotInPath(node: Node) {
      let count = 0;
      for (let n of node.pairs) {
        if (this.path.contains(n.value)) {
          continue;
        }
        count++;
      }
      return count;
    }
  
    constructor(path: Path) {
      this.path = path;
    }
  
    compare(i: Node, j: Node) {
      let a = this.pairsNotInPath(i);
      let b = this.pairsNotInPath(j);
      if (a != b) {
        if (a < b) {
          return -1;
        }
        return 1;
      }

      a = i.pairsCount();
      b = i.pairsCount();

      if (a < b) {
        return -1;
      }
      return 1;
    }
  }