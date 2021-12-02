import { Path } from './Path';
import { Node } from './Node';

export class NodesComparer {
    private _path?: Path | null;
    private _cache: Array<number>;

    constructor(path: Path | null, maxN: number) {
        this._cache = new Array(maxN + 1);
        this._path = path;
    }

    flushCache() {
        if (!this._path) {
            return;
        }
        for(let i = 0; i < this._cache.length; i++) {
            this._cache[i] = -1;
        }
    }

    compare(x: Node, y: Node): number {
        if (this._path) {
            let a = this.getFromCache(x);
            let b = this.getFromCache(y);
            if (a != b) {
                if (a < b) {
                    return -1;
                }
                if (a > b) {
                    return 1;
                }
            }
        }

        let a = x.pairsCount();
        let b = y.pairsCount();
        if (a != b) {
            if (a < b) {
                return -1;
            }
            if (a > b) {
                return 1;
            }
        }

        a = x.value;
        b = y.value;

        if (a > b) {
            return -1;
        }
        if (a < b) {
            return 1;
        }
        return 0;
    }

    private getFromCache(node: Node): number {
        let result = this._cache[node.value];
        if (result === -1) {
            result = node.pairsNotInPath(this._path);
            this._cache[node.value] = result;
        }
        return result;
    }
}