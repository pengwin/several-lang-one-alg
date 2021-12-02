use square_sums::Metrics;
use square_sums::square_sums_row;

fn print(s: String) {
    println!("{}", s);
}

fn main() {
    let mut metrics = Some(Metrics::new(false, print));
    let mut count = 0;
    for n in 2..2001 {
        match square_sums_row::<_, square_sums::NativeNodesSorting, square_sums::NativeNodesSortingWithCache>(n, &mut metrics) {
            Ok(res) => match res {
                Some(_) => count+=1,
                None => {},
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
