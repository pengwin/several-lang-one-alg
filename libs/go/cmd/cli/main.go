package main

import (
	sums "gitlab.com/ikruchkov0/codewars_katas/go/square-sums-simple/pkg"
)

func main() {

	/*sums.SquareSumsRow(279)
	return*/

	for n := 0; n < 2000; n++ {
		a := sums.SquareSumsRow(n)
		if a == nil {
			// no
			//fmt.Printf("No")
		} else {
			// yes
			//fmt.Printf("%+v", a)
		}
		//fmt.Printf("%d\n", n)
	}
}
