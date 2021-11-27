import { Tree } from './Tree';
import { Node } from './Node';
import { Path } from './Path';
import { NodesSorting, NodesSortingFactory } from './NodesSorting';
import { Metrics } from './Metrics';

function isFairSquare(n: number) {
    let sqrtVal = Math.sqrt(n);
    return sqrtVal - Math.floor(sqrtVal) == 0;
}

function buildTree(n: number, sorting: NodesSorting) {
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

    tree.sortPairsWithSorting(sorting);
    return tree;
}

function dfs(n: number, node: Node, path: Path, metrics: Metrics | null, sorting: NodesSorting) {
    metrics?.incrementDfsCounter();

    sorting.sortNodes(node.pairs);

    for (let p of node.pairs) {
        let v = p.value;

        if (path.contains(v)) {
            continue;
        }

        path.push(v);

        if (path.count == n) {
            break;
        }

        dfs(n, p, path, metrics, sorting);
        if (path.count == n) {
            break;
        }

        path.pop();
    }
}

export function squareSumsRow(n: number, metrics: Metrics | null, sortingFactory: NodesSortingFactory) {
    let sortingForTree = sortingFactory(null, n);
    let tree = buildTree(n, sortingForTree);
    if (!tree) {
        return false;
    }

    for (let root of tree.nodes) {
        const path = new Path(n);
        const sorting = sortingFactory(path, n);
        path.push(root.value);
        dfs(n, root, path, metrics, sorting);
        if (path.count == n) {
            let result = path.toVector();
            metrics.finalizeDfsCounter(n);
            return result;
        }
    }
    return false;
}