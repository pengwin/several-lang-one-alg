package main

import (
	"fmt"
	"os"
	"strconv"

	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

func sortingFactory(path *sums.Path, maxN int) sums.NodesSortingFacade {
	return *sums.NewNodesSortingFacade(path, maxN)
}

func convertWithPanic(str string) int {
	res, err := strconv.Atoi(str)
	if err != nil {
		panic("Unable to convert argument")
	}
	return res
}

func main() {

	args := os.Args[1:]

	from := 2
	to := 2000

	if len(args) >= 2 {
		from = convertWithPanic(args[0])
		to = convertWithPanic(args[1])
	}

	fmt.Printf("Calculating from: %d to: %d\n", from, to)

	metrics := sums.NewMetrics(false)

	for n := from; n <= to; n++ {
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
