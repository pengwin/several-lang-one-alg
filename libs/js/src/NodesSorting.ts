import { Path } from './Path';
import { Node } from './Node';
import { CustomNodesSorting } from './CustomNodesSorting';
import { NativeNodesSorting } from './NativeNodesSorting';
import { QSortNodesSorting } from './QSortNodesSorting';

export interface NodesSorting {
    sortNodes(nodes: Array<Node>);
}

export type NodesSortingFactory = (path: Path | null, maxN: number) => NodesSorting;

export class NodesSortingFactories {
    static CreateCustom(path: Path | null, maxN: number): NodesSorting {
        return new CustomNodesSorting(path, maxN);
    }

    static CreateNative(path: Path | null, maxN: number): NodesSorting {
        return new NativeNodesSorting(path, maxN);
    }

    static CreateQSort(path: Path | null, maxN: number): NodesSorting {
        return new QSortNodesSorting(path, maxN);
    }
}