import { Metrics } from './Metrics';
import { squareSumsRow } from './SquareSums';
import { NodesSortingFactories } from './NodesSorting';

function main(args: Array<string>) {

  let from = 2;
  let to = 2000;

  if (args.length >= 4) {
    from = parseInt(args[2]);
    to = parseInt(args[3]);
  }

  console.log(`Calculating from: ${from} to: ${to}`);

  let metrics = new Metrics(false);
  //return squareSumsRow(886);
  
  for (let n = from; n <= to; n++) {
    let res = squareSumsRow(n, metrics, NodesSortingFactories.CreateNative);
  }
  metrics.printMetrics();
}

main(process.argv);


