import { Path } from './Path';
import { Node } from './Node';
import { NodesComparer } from './NodesComparer';

function partition(comparer: NodesComparer, nodes: Array<Node>, start: number, end: number): number {
    let marker = start;
    for (let i = start; i < end; i++) {
        var compareResult = comparer.compare(nodes[i], nodes[end]);
        if (compareResult == -1) {
            let temp = nodes[marker]; 
            nodes[marker] = nodes[i];
            nodes[i] = temp;
            marker += 1;
        }
    }

    let temp = nodes[marker];
    nodes[marker] = nodes[end];
    nodes[end] = temp; 
    return marker;
}

function quicksort(comparer: NodesComparer, nodes: Array<Node>, start: number, end: number) {
    if (start >= end) {
        return;
    }

    let pivot = partition(comparer, nodes, start, end);
    quicksort(comparer, nodes, start, pivot - 1);
    quicksort(comparer, nodes, pivot + 1, end);
}

export class QSortNodesSorting {
    private _comparer: NodesComparer;

    constructor(path: Path | null, maxN: number) {
        this._comparer = new NodesComparer(path, maxN);
    }

    public sortNodes(nodes: Array<Node>) {
        this._comparer.flushCache();
        quicksort(this._comparer, nodes, 0, nodes.length - 1);
    }
}