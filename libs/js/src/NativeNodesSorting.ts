import { Path } from './Path';
import { Node } from './Node';
import { NodesComparer } from './NodesComparer';


export class NativeNodesSorting {
    private _comparer: NodesComparer;

    constructor(path: Path | null, maxN: number) {
        this._comparer = new NodesComparer(path, maxN);
    }

    public sortNodes(nodes: Array<Node>) {
        this._comparer.flushCache();
        nodes.sort((a, b) => this._comparer.compare(a, b));
    }
}