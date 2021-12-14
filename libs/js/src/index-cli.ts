import { squareSumsRow } from './sums';
function main(args: Array<string>) {

  let from = 2;
  let to = 2000;

  if (args.length >= 4) {
    from = parseInt(args[2]);
    to = parseInt(args[3]);
  }

  console.log(`Calculating from: ${from} to: ${to}`);

  
  for (let n = from; n <= to; n++) {
    let res = squareSumsRow(n);
  }
}

main(process.argv);


