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
  
    compare(i, j) {
      if (this.pairsNotInPath(i) < this.pairsNotInPath(j)) {
        return -1;
      }
      return 1;
    }
  }