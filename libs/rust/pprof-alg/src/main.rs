use std::env;
use std::fs::File;
use std::io::Write;
use std::path;

use pprof::Symbol;
use pprof::protos::Message;
use square_sums::square_sums_row;
use square_sums::Metrics;

fn print(s: String) {
    println!("{}", s);
}


fn frames_post_processor(pprof_frames: &mut pprof::Frames) {
    let mut frames: Vec<Vec<Symbol>> = vec![];
    for f in &pprof_frames.frames {
        let mut symbols: Vec<Symbol> = vec![];
        for s in f {
            if let Some(buf) = &s.name {
                let name = match std::str::from_utf8(&buf) {
                    Ok(v) => v,
                    Err(e) => panic!("Invalid UTF-8 sequence: {}", e),
                };
                if !name.contains("backtrace") {
                    symbols.push(s.clone());
                }
            }
        }
        frames.push(symbols);
    }
    pprof_frames.frames = frames;
}

fn main() {
    let mut outfile = "".to_owned();
    let outfile_arg = env::args()
        .into_iter()
        .nth(1)
        .expect("Output file path should be specified");
    let outfile_path = path::Path::new(&outfile_arg);
    if !outfile_path.is_absolute() {
        let file_name = outfile_path
            .file_name()
            .expect("No file name specified in path");
        let parent = outfile_path
            .parent()
            .expect("Parent fir is not specified")
            .canonicalize()
            .expect("Wrong Path");
        let full_path = parent.join(&file_name);
        outfile = full_path
            .as_path()
            .to_str()
            .expect("Should be string")
            .to_string();
    }
    println!("Output file: {:?}", outfile);

    let guard = pprof::ProfilerGuardBuilder::default()
        .frequency(100)
        .blocklist(&["libc", "libgcc", "pthread", "backtrace"],)
        .build()
        .unwrap();

    work(1500, 1510);

    match guard.report().frames_post_processor(frames_post_processor).build() {
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

fn work(from: u32, to: u32) {
    let mut metrics = Some(Metrics::new(true, print));
    let mut count = 0;
    for n in from..(to+1) {
        match square_sums_row(n, &mut metrics) {
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
