import { Tree } from './Tree';
import { Node } from './Node';
import { Path } from './Path';
import { NodesSorting } from './NodesSorting';
import { Metrics } from './Metrics';

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

    let sorting = new NodesSorting(null, n);
    tree.sortPairsWithSorting(sorting);
    return tree;
}

function dfs(n: number, node: Node, path: Path, metrics?: Metrics) {
    metrics?.incrementDfsCounter();

    let sorting = new NodesSorting(path, n);
    let pairs = sorting.sortNodes(node.pairs);

    for (let p of pairs) {
        let v = p.value;

        if (path.contains(v)) {
            continue;
        }

        path.push(v);

        if (path.count == n) {
            break;
        }

        dfs(n, p, path, metrics);
        if (path.count == n) {
            break;
        }

        path.pop();
    }
}

export function squareSumsRow(n: number, metrics?: Metrics) {
    let tree = buildTree(n);
    if (!tree) {
        return false;
    }

    for (let root of tree.nodes) {
        var path = new Path(n);
        path.push(root.value);
        dfs(n, root, path, metrics);
        if (path.count == n) {
            let result = path.toVector();
            metrics.finalizeDfsCounter(n);
            return result;
        }
    }
    return false;
}