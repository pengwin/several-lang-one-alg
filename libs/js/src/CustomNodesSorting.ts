import { Path } from './Path';
import { Node } from './Node';

class SortBucketList {
    private _path?: Path;
    private list: Array<Node>;

    constructor(path?: Path) {
        this._path = path;
        this.list = [];
    }

    public nodes() {
        return this.list;
    }

    public addNode(node: Node) {
        let found = false;
        for (let i = 0; i < this.list.length; i++)
        {
            var currentNode = this.list[i];
            let condition = false;
            if (this._path != null) {
                var a = node.pairsCount();
                var b = currentNode.pairsCount();
                if (a != b) {
                    condition = a < b;
                }
                else {
                    condition = node.value > currentNode.value;
                }
            }
            else {
                condition = node.value > currentNode.value;
            }

            if (condition) {
                this.list.splice(i, 0, node);
                found = true;
                break;
            }
        }

        // reached end of list without adding
        if (!found) {
            this.list.push(node);
        }
    }
}

export class CustomNodesSorting {
    private _path?: Path | null;
    private _maxN: number;

    constructor(path: Path | null, maxN: number) {
        this._path = path;
        this._maxN = maxN;
    }

    public sortNodes(nodes: Array<Node>){
        const sortList: Array<SortBucketList> = new Array(this._maxN + 1);

        for (let i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            var pairsCount = this._path == null ? node.pairsCount() : node.pairsNotInPath(this._path);

            var list = sortList[pairsCount];
            if (list == null) {
                list = new SortBucketList(this._path);
            }

            list.addNode(node);

            sortList[pairsCount] = list;
        }

        let index = 0;
        for (let list of sortList) {
            if (list == null) {
                continue;
            }

            for(let node of list.nodes()) {
                nodes[index] = node;
                index+=1;
            }
        }
    }
}
