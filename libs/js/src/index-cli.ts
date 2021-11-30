import { Metrics } from './Metrics';
import { squareSumsRow } from './SquareSums';
import { NodesSortingFactories } from './NodesSorting';

function main() {

  let metrics = new Metrics(false);
  //return squareSumsRow(886);
  
  for (let n = 2; n <= 2000; n++) {
    let res = squareSumsRow(n, metrics, NodesSortingFactories.CreateNative);
  }
  metrics.printMetrics();
}

main();


