export class Metrics {
    private printProgress: boolean;
    private dfsCounter: number = 0;
    private dfsCounterMap: Map<number, number> = new Map();

    constructor(printProgress: boolean) {
        this.printProgress = printProgress;
    }

    public incrementDfsCounter() {
        this.dfsCounter += 1;
    }

    public finalizeDfsCounter(n: number) {
        this.dfsCounterMap.set(n, this.dfsCounter);
        if (this.printProgress) {
            console.log(`Done: ${n}: dfs counter ${this.dfsCounter}`);
        }
        this.dfsCounter = 0;
    }

    public printMetrics() {
        let badCounter = 0;
        let normalCounter = 0;
        let averageCounter = 0;
        let maxN = 0;
        let worst = 0;
        let worstN = 0;
        for(let n of this.dfsCounterMap.keys()) {
            if (n > maxN) {
                maxN = n;
            }
            const dfsCounter = this.dfsCounterMap.get(n);
            if (dfsCounter > worst) {
                worst = dfsCounter;
                worstN = n;
            }
            if (dfsCounter / n  > 3) {
                //console.log(`Counter for ${n}: ${dfsCounter}`);
                badCounter++;
            } else  if (dfsCounter <= n) {
                normalCounter++;
            } else {
                averageCounter++;
            }
            
        }
        console.info(`Normal (*<=n) dfs cases count: ${normalCounter}/${maxN}`);
        console.info(`Average (n<*<3n) dfs cases count: ${averageCounter}/${maxN}`);
        console.info(`Bad dfs (*>3n) cases count: ${badCounter}/${maxN}`);
        console.warn(`Worst dfs case: ${worst} for ${worstN}`);
    }
}