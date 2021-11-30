use metrics::Metrics;

mod metrics;
mod node;
mod nodes_sorting;
mod path;
mod square_sums;
mod tree;

fn main() {
    let mut metrics: Option<Metrics> = Some(Metrics::new(false));
    let mut count = 0;
    for n in 2..2001 {
        match square_sums::square_sums_row(n, &mut metrics) {
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
