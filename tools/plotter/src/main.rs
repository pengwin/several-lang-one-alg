mod draw_chart;
mod models;

use std::path::PathBuf;

use models::LangDef;

fn duration_extractor(l: &LangDef) -> f64 {
    l.duration
}

fn max_mem_extractor(l: &LangDef) -> f64 {
    l.max_mem
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let args: Vec<String> = std::env::args().collect();
    if args.len() < 4 {
        println!("Not enough arguments");
        return Ok(());
    }

    let binary = models::read_binary_metrics(&args[1])?;
    let wasm = models::read_wasm_metrics(&args[2])?;
    let combined = models::combine(&binary, &wasm);
    let combined_wo_blazor = models::exclude(&combined, &["blazor", ".net", "uno"]);

    let histograms_path = PathBuf::from(&args[3]);

    let margin = 10;

    draw_chart::draw_chart(
        &histograms_path.join("binary_duration.png"),
        &binary,
        "Binary",
        "Seconds",
        margin,
        duration_extractor,
    )?;
    draw_chart::draw_chart(
        &histograms_path.join("binary_mem.png"),
        &binary,
        "Binary",
        "Kb",
        margin,
        max_mem_extractor,
    )?;
    draw_chart::draw_chart(
        &histograms_path.join("wasm_duration.png"),
        &wasm,
        "WASM",
        "Seconds",
        margin,
        duration_extractor,
    )?;
    draw_chart::draw_chart(
        &histograms_path.join("combined_wo_dotnet_duration.png"),
        &combined_wo_blazor,
        "Binary + WASM w/o .Net",
        "Seconds",
        margin,
        duration_extractor,
    )?;
    draw_chart::draw_chart(
        &histograms_path.join("combined_duration.png"),
        &combined,
        "Binary + WASM",
        "Seconds",
        margin,
        duration_extractor,
    )?;

    Ok(())
}
