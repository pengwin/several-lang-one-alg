import { Path } from './Path';
import { Node } from './Node';

export class NodesComparer {
    private _path?: Path | null;
    private _cache: Array<number>;
    private _maxCachePosition: number;

    constructor(path: Path | null, maxN: number) {
        let cache = new Array(maxN + 1);
        for(let i = 0; i < cache.length; i++) {
            cache[i] = -1;
        }
        this._cache = cache;
        this._path = path;
        this._maxCachePosition = 0;
    }

    flushCache() {
        if (!this._path) {
            return;
        }
        if (this._maxCachePosition == 0) {
            return;
        }
        for(let i = 0; i < this._cache.length; i++) {
            this._cache[i] = -1;
        }
        this._maxCachePosition = 0;
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
            if (node.value > this._maxCachePosition) {
                this._maxCachePosition = node.value;
            }
        }
        return result;
    }
}