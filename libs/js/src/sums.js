class Node {
  constructor(val) {
    this.value = val;
    this.pairs = [];
  }

  sortPairs() {
    this.pairs = this.pairs.sort((a, b) => Node.compareNodes(a, b));
  }

  pairsCount() {
    return this.pairs.length;
  }

  add(node) {
    this.pairs.push(node);
  }

  static compareNodes(a, b) {
    if (a.pairsCount() < b.pairsCount()) {
      return -1;
    }
    return 1;
  }
}

class Tree {
  constructor(n) {
    this.nodes = Array(n).fill(null);
  }

  getOrCreate(n) {
    let node = this.nodes[n - 1];
    if (!node) {
      node = new Node(n);
      this.nodes[n - 1] = node;
    }
    return node;
  }

  addPair(head, tail) {
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

class PathNode {
  constructor(n, prevNode) {
    this.value = n;
    this.prev = prevNode;
  }
}

class Path {
  constructor(capacity) {
    this.count = 0;
    this.last = null;
    this.attached = Array(capacity).fill(false);
  }

  _toVector(node, v) {
    let prev = node;
    while (prev) {
      v.push(prev.value);
      prev = prev.prev;
    }
    return v;
  }

  contains(n) {
    return this.attached[n];
  }

  push(n) {
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

class NodesComparer {
  pairsNotInPath(node) {
    let count = 0;
    for (let n of node.pairs) {
      if (this.path.contains(n.value)) {
        continue;
      }
      count++;
    }
    return count;
  }

  constructor(path) {
    this.path = path;
  }

  compare(i, j) {
    if (this.pairsNotInPath(i) < this.pairsNotInPath(j)) {
      return -1;
    }
    return 1;
  }
}

function isFairSquare(n) {
  let sqrtVal = Math.sqrt(n);
  return sqrtVal - Math.floor(sqrtVal) == 0;
}

function buildTree(n) {
  let tree = new Tree(n);

  for (let i = 1; i <= n; i++) {
    for (let j = 1; j <= n; j++) {
      if (i == j) {
        continue;
      }

      let sum = i + j;
      if (!isFairSquare(sum)) {
        continue;
      }

      tree.addPair(i, j);
    }
  }

  if (!tree.verifyAllNodesHavePairs()) {
    return false;
  }

  tree.sortPairs();
  return tree;
}

function dfs(n, node, path) {
  let comparer = new NodesComparer(path);
  let pairs = [...node.pairs].sort((a, b) => comparer.compare(a, b));

  for (let p of pairs) {
    let v = p.value;

    if (path.contains(v)) {
      continue;
    }

    path.push(v);

    if (path.count == n) {
      return;
    }

    dfs(n, p, path);
    if (path.count == n) {
      return;
    }

    path.pop();
  }
}

export function squareSumsRow(n) {
  let tree = buildTree(n);
  if (!tree) {
    return false;
  }

  for (let root of tree.nodes) {
    var path = new Path(n);
    path.push(root.value);
    dfs(n, root, path);
    if (path.count == n) {
      let result = path.toVector();
      return result;
    }
  }
  return false;
}

