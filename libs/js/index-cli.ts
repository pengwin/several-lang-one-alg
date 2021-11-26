import { Metrics } from './src/Metrics';
import { squareSumsRow } from './src/SquareSums';

function main() {

  let metrics = new Metrics(false);
  //return squareSumsRow(886);
  
  for (let n = 2; n <= 2000; n++) {
    let res = squareSumsRow(n, metrics);
  }
  metrics.printMetrics();
}

main();


