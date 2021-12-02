import { Metrics } from './Metrics';
import { squareSumsRow } from './SquareSums';
import { NodesSortingFactories } from './NodesSorting';

export function fullSquareSums(from: number, to: number) {

  let metrics = new Metrics(false);
  
  for (let n = 2; n <= 2000; n++) {
    let res = squareSumsRow(n, metrics, NodesSortingFactories.CreateQSort);
  }
  metrics.printMetrics();
}