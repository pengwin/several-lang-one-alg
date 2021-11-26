package sums

import "fmt"

type Metrics struct {
	printProgress bool
	dfsCounter    int
	dfsCounterMap map[int]int
}

func NewMetrics(printProgress bool) *Metrics {
	return &Metrics{
		printProgress: printProgress,
		dfsCounter:    0,
		dfsCounterMap: make(map[int]int),
	}
}

func (rcvr *Metrics) IncrementDfsCounter() {
	rcvr.dfsCounter += 1
}

func (rcvr *Metrics) FinalizeDfsCounter(n int) {
	rcvr.dfsCounterMap[n] = rcvr.dfsCounter
	if rcvr.printProgress {
		fmt.Printf("Done: %d: dfs counter %d\n", n, rcvr.dfsCounter)
	}
	rcvr.dfsCounter = 0
}

func (rcvr *Metrics) PrintMetrics() {
	badCounter := 0
	normalCounter := 0
	averageCounter := 0
	maxN := 0
	worst := 0
	worstN := 0
	for n, dfsCounter := range rcvr.dfsCounterMap {
		if n > maxN {
			maxN = n
		}
		if dfsCounter > worst {
			worst = dfsCounter
			worstN = n
		}
		if dfsCounter/n > 3 {
			badCounter++
		} else if dfsCounter <= n {
			normalCounter++
		} else {
			averageCounter++
		}
	}
	fmt.Printf("Normal (*<=n) dfs cases count: %d/%d\n", normalCounter, maxN)
	fmt.Printf("Average (n<*<3n) dfs cases count: %d/%d\n", averageCounter, maxN)
	fmt.Printf("Bad dfs (*>3n) cases count: %d/%d\n", badCounter, maxN)
	fmt.Printf("Worst dfs case: %d for %d\n", worst, worstN)
}
