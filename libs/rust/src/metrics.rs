use std::collections::HashMap;

pub struct Metrics {
    print_progress: bool,
    dfs_counter_map: HashMap<u32, u32>,
    dfs_counter: u32,
}

impl Metrics {
    pub fn new(print_progress: bool) -> Metrics {
        Metrics {
            print_progress,
            dfs_counter: 0,
            dfs_counter_map: HashMap::new(),
        }
    }

    pub fn increment_dfs_counter(&mut self) {
        self.dfs_counter += 1
    }

    pub fn finalize_dfs_counter(&mut self, n: u32) {
        self.dfs_counter_map.insert(n, self.dfs_counter);
        if self.print_progress {
            println!("Done: {}: dfs counter {}", n, self.dfs_counter);
        }

        self.dfs_counter = 0
    }

    pub fn print_metrics(&self) {
        let mut bad_counter = 0;
        let mut normal_counter = 0;
        let mut average_counter = 0;
        let mut max_n = 0;
        let mut worst = 0;
        let mut worst_n = 0;
        for (n, dfs_counter) in &self.dfs_counter_map {
            if *n > max_n {
                max_n = *n;
            }

            if *dfs_counter > worst {
                worst = *dfs_counter;
                worst_n = *n;
            }

            if dfs_counter / n > 3 {
                bad_counter += 1;
            } else if dfs_counter <= n {
                normal_counter += 1;
            } else {
                average_counter += 1;
            }
        }

        println!(
            "Normal (*<=n) dfs cases count: {}/{}",
            normal_counter, max_n
        );
        println!(
            "Average (n<*<3n) dfs cases count: {}/{}",
            average_counter, max_n
        );
        println!("Bad dfs (*>3n) cases count: {}/{}", bad_counter, max_n);
        println!("Worst dfs case: {} for {}", worst, worst_n);
    }
}
