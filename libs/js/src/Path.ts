import { Node } from './Node';
import { PathNode } from './PathNode';

export class Path {
    public count: number;
    private last?: PathNode;
    private attached: Array<boolean>;

    constructor(capacity: number) {
      this.count = 0;
      this.last = null;
      this.attached = Array(capacity).fill(false);
    }
  
    _toVector(node: PathNode, v: Array<number>) {
      let prev = node;
      while (prev) {
        v.push(prev.value);
        prev = prev.prev;
      }
      return v;
    }
  
    contains(n: number) {
      return this.attached[n];
    }
  
    push(n: number) {
      if (this.attached[n]) {
        throw 'Already attached';
      }
  
      let prev = this.last;
      this.last = new PathNode(n, prev);
  
      this.attached[n] = true;
      this.count++;
    }
  
    pop() {
      if (!this.last) {
        return;
      }
      this.attached[this.last.value] = false;
      let prev = this.last.prev;
      this.last = prev;
      this.count--;
    }
  
    toVector() {
      return this._toVector(this.last, []).reverse();
    }
  }