use std::env;

use square_sums::square_sums_row;
use square_sums::Metrics;

fn print(s: String) {
    println!("{}", s);
}

fn main() {
    let args: Vec<String> = env::args().collect();

    let mut from = 2;
    let mut to = 2000;

    if args.len() >= 3 {
        from = args[1].parse::<u32>().expect("Unable to parse from");
        to = args[2].parse::<u32>().expect("Unable to parse from");
    }

    println!("Calculating from: {} to: {}", from, to);

    let mut metrics = Some(Metrics::new(false, print));
    let mut count = 0;
    for n in from..(to+1) {
        match square_sums_row(n, &mut metrics) {
            Ok(res) => match res {
                Some(_) => count += 1,
                None => {}
            },
            Err(e) => eprintln!("{}", e),
        }
    }

    if count == 0 {
        panic!("Wrong result count == 0")
    }

    if let Some(m) = metrics.as_mut() {
        m.print_metrics();
    }
}
