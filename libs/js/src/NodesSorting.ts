import { Path } from './Path';
import { Node } from './Node';

function pairsNotInPath(node: Node, path: Path): number {
    let count = 0;
    for (let n of node.pairs) {
        if (path.contains(n.value)) {
            continue;
        }
        count++;
    }
    return count;
}

class SortBucket {
    public value: Node;

    public next?: SortBucket;

    constructor(value: Node) {
        this.value = value;
    }
}

class SortBucketList {
    private _path?: Path;
    public first?: SortBucket;

    constructor(path?: Path) {
        this._path = path;
    }

    public addNode(node: Node) {
        if (this.first == null) {
            this.first = new SortBucket(node);
            return;
        }

        let current = this.first;
        let prev: SortBucket = null;
        while (current != null) {
            let condition: boolean;
            if (this._path != null) {
                var a = node.pairsCount();
                var b = current.value.pairsCount();
                if (a != b) {
                    condition = a < b;
                }
                else {
                    condition = node.value > current.value.value;
                }
            }
            else {
                condition = node.value > current.value.value;
            }

            if (condition) {
                var next = current;
                current = new SortBucket(node);
                current.next = next;
                if (prev != null) {
                    prev.next = current;
                }
                else {
                    this.first = current;
                }
                break;
            }

            prev = current;
            current = current.next;
        }

        // reached end of list without adding
        if (current == null && prev != null) {
            prev.next = new SortBucket(node);
        }
    }
}

export class NodesSorting {
    private _path?: Path | null;
    private _maxN: number;

    constructor(path: Path | null, maxN: number) {
        this._path = path;
        this._maxN = maxN;
    }

    public sortNodes(nodes: Array<Node>): Array<Node> {
        const sortList: Array<SortBucketList> = new Array(this._maxN + 1);

        for (let i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            var pairsCount = this._path == null ? node.pairsCount() : pairsNotInPath(node, this._path);

            var list = sortList[pairsCount];
            if (list == null) {
                list = new SortBucketList(this._path);
            }

            list.addNode(node);

            sortList[pairsCount] = list;
        }

        const result = [];
        for (let list of sortList) {
            if (list == null) {
                continue;
            }

            var current = list.first;
            while (current != null) {
                result.push(current.value);
                current = current.next;
            }
        }
        return result;
    }
}
