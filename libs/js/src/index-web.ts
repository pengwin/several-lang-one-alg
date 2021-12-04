import { Metrics } from './Metrics';
import { squareSumsRow } from './SquareSums';
import { NodesSortingFactories } from './NodesSorting';

export function fullSquareSums(from: number, to: number) {

  let metrics = new Metrics(false);
  
  for (let n = from; n <= to; n++) {
    let res = squareSumsRow(n, metrics, NodesSortingFactories.CreateNative);
  }
  metrics.printMetrics();
}