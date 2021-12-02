mod utils;

use wasm_bindgen::prelude::*;

use square_sums::square_sums_row;
use square_sums::Metrics;

use web_sys;

// When the `wee_alloc` feature is enabled, use `wee_alloc` as the global
// allocator.
#[cfg(feature = "wee_alloc")]
#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;


fn print(s: String) {
    web_sys::console::log_1(&s.into());
}

fn err(s: String) {
    web_sys::console::error_1(&s.into());
}

#[wasm_bindgen]
pub fn square_sums(from: u32, to: u32) -> u32 {
    let mut metrics = Some(Metrics::new(false, print));
    let mut count = 0;
    for n in from..to+1 {
        match square_sums_row(n, &mut metrics) {
            Ok(res) => match res {
                Some(_) => count+=1,
                None => {},
            },
            Err(e) => err(e),
        }
    }


    if let Some(m) = metrics.as_mut() {
        m.print_metrics();
    }

    count
}
