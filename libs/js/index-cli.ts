









import { squareSumsRow } from './src/SquareSums';

function main() {

  //return squareSumsRow(886);

  //console.time('Total');
  for (let n = 2; n <= 2000; n++) {
    //console.time(n.toString());
    let res = squareSumsRow(n);
    //console.timeEnd(n.toString());
  }
  //console.timeEnd('Total');
}

main();


