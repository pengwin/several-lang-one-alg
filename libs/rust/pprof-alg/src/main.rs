use std::fs::File;
use std::io::Write;
use std::env;
use std::path;

use pprof::protos::Message;
use square_sums::Metrics;
use square_sums::square_sums_row;

fn print(s: String) {
    println!("{}", s);
}

fn main() {
    let mut outfile = "".to_owned();
    let outfile_arg = env::args().into_iter().nth(1).expect("Output file path should be specified");
    let outfile_path = path::Path::new(&outfile_arg);
    if !outfile_path.is_absolute() {
        let file_name = outfile_path.file_name().expect("No file name specified in path");
        let parent = outfile_path.parent().expect("Parent fir is not specified").canonicalize().expect("Wrong Path");
        let full_path = parent.join(&file_name);
        outfile = full_path.as_path().to_str().expect("Should be string").to_string();
    }
    println!("Output file: {:?}", outfile);
    
    let guard = pprof::ProfilerGuard::new(100).unwrap();

    work();

    match guard.report().build() {
        Ok(report) => {
            let mut file = File::create(outfile).unwrap();
            let profile = report.pprof().unwrap();
    
            let mut content = Vec::new();
            profile.encode(&mut content).unwrap();
            file.write_all(&content).unwrap();
    
            //println!("report: {:?}", &report);
        }
        Err(_) => {}
    };

    
}

fn work() {
    let mut metrics = Some(Metrics::new(false, print));
    let n = 102;
    match square_sums_row::<_, square_sums::NativeNodesSorting, square_sums::NativeNodesSortingWithCache>(n, &mut metrics) {
        Ok(res) => match res {
            Some(_) => {},
            None => {},
        },
        Err(e) => eprintln!("{}", e),
    }

    if let Some(m) = metrics.as_mut() {
        m.print_metrics();
    }
}
