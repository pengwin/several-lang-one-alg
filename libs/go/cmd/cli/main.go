package main

import (
	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

func sortingFactory(path *sums.Path, maxN int) sums.NodesSorting {
	//return sums.NewNodesSortingCustom(path, maxN)
	return sums.NewNodesSortingNative(path, maxN)
}

func main() {

	metrics := sums.NewMetrics(false)
	/*sums.SquareSumsRow(279)
	return*/

	for n := 500; n <= 1000; n++ {
		a := sums.SquareSumsRow(n, metrics, sortingFactory)
		if a == nil {
			// no
			//fmt.Printf("No")
		} else {
			// yes
			//fmt.Printf("%+v", a)
		}
		//fmt.Printf("%d\n", n)
	}

	metrics.PrintMetrics()
}
