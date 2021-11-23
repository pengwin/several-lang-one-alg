import { Tree } from './Tree';
import { Node } from './Node';
import { Path } from './Path';
import { NodesInPathComparer } from './NodesInPathComparer';

function isFairSquare(n: number) {
    let sqrtVal = Math.sqrt(n);
    return sqrtVal - Math.floor(sqrtVal) == 0;
}

function buildTree(n: number) {
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

function dfs(n: number, node: Node, path: Path) {
    let comparer = new NodesInPathComparer(path);
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

export function squareSumsRow(n: number) {
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