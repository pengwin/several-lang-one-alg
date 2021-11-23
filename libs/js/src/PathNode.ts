export class PathNode {
    public readonly value: number;
    public readonly prev?: PathNode;

    constructor(n: number, prevNode?: PathNode) {
      this.value = n;
      this.prev = prevNode;
    }
  }