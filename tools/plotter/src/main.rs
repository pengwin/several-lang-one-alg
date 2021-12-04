use plotters::prelude::*;
#[derive(Copy, Clone)]
struct LangDef {
    pub color: RGBColor,
    pub name: &'static str,
    duration: f64,
}

fn calc_max(data: &[LangDef]) -> f64 {
    let mut max = 0f64;
    for f in data {
        if f.duration > max {
            max = f.duration;
        }
    }

    max + max * 0.2f64
}

fn build_x_spec(data: &[LangDef]) -> Vec<String> {
    let mut res = vec![];
    for f in data {
        res.push(f.name.to_owned());
    }
    res
}

fn draw_chart(file_path: &str, langs: &[LangDef], caption: &str, margin: u32) -> Result<(), Box<dyn std::error::Error>> {
    let root = BitMapBackend::new(file_path, (1024, 480)).into_drawing_area();

    let max = calc_max(langs);
    
    let x_spec = build_x_spec(langs);

    root.fill(&WHITE)?;

    let mut chart = ChartBuilder::on(&root)
        .x_label_area_size(35)
        .y_label_area_size(40)
        .margin(5)
        .caption(caption, ("sans-serif", 50.0))
        .build_cartesian_2d(x_spec.into_segmented(), 0f64..max)?;

    chart
        .configure_mesh()
        .disable_x_mesh()
        .bold_line_style(&WHITE.mix(0.3))
        .y_desc("Seconds")
        .x_desc("Langs")
        .axis_desc_style(("sans-serif", 15))
        .draw()?;

    for f in langs {
        chart.draw_series(
            Histogram::vertical(&chart)
                .margin(margin)
                .style(f.color.mix(0.9).filled())
                .data(
                    x_spec
                        .iter()
                        .filter(|lang| **lang == f.name)
                        .map(|lang| (lang, f.duration)),
                ),
        )?;
    }

    // To avoid the IO failure being ignored silently, we manually call the present function
    root.present().expect("Unable to write result to file, please make sure 'plotters-doc-data' dir exists under current dir");
    println!("Result has been saved to {}", file_path);

    Ok(())
}

fn main() -> Result<(), Box<dyn std::error::Error>> {

    let binary = [
        LangDef {
            color: RGBColor(200, 0, 0),
            duration: 8.61,
            name: "C++",
        },
        LangDef {
            color: RGBColor(125, 0, 0),
            duration: 12.18,
            name: "Go",
        },
        LangDef {
            color: RGBColor(0, 125, 200),
            duration: 14.11,
            name: ".NET",
        },
        LangDef {
            color: RGBColor(0, 200, 0),
            duration: 15.32,
            name: "Node.js",
        },
        LangDef {
            color: RGBColor(0, 125, 125),
            duration: 9.13,
            name: "Rust",
        },
    ];

    let wasm = [
        LangDef {
            color: RGBColor(200, 0, 0),
            duration: 19.55,
            name: "C++ (Wasm)",
        },
        LangDef {
            color: RGBColor(125, 0, 0),
            duration: 65.35,
            name: "Go (Wasm)",
        },
        LangDef {
            color: RGBColor(0, 125, 200),
            duration:  588.62,
            name: "Blazor (AOT)",
        },
        LangDef {
            color: RGBColor(0, 125, 125),
            duration: 825.09,
            name: "Blazor",
        },
        LangDef {
            color: RGBColor(0, 200, 0),
            duration: 15.32,
            name: "JS (Browser)",
        },
        LangDef {
            color: RGBColor(0, 125, 125),
            duration: 30.93,
            name: "Rust (Wasm)",
        },
    ];

    let mut combined = vec![];
    combined.push(binary[0]); // C++
    combined.push(wasm[0]);

    combined.push(binary[1]); // Go
    combined.push(wasm[1]);

    combined.push(binary[2]); // .Net
    combined.push(wasm[2]);
    combined.push(wasm[3]);

    combined.push(binary[3]); // Js
    combined.push(wasm[4]);

    combined.push(binary[4]); // Rust
    combined.push(wasm[5]);

    let mut combined_wo_dotnet = vec![];
    combined_wo_dotnet.push(binary[0]); // C++
    combined_wo_dotnet.push(wasm[0]);

    combined_wo_dotnet.push(binary[1]); // Go
    combined_wo_dotnet.push(wasm[1]);


    combined_wo_dotnet.push(binary[3]); // Js
    combined_wo_dotnet.push(wasm[4]);

    combined_wo_dotnet.push(binary[4]); // Rust
    combined_wo_dotnet.push(wasm[5]);

    draw_chart("./histograms/binary.png", &binary, "Binary", 150)?;
    draw_chart("./histograms/wasm.png", &wasm, "WASM", 130)?;
    draw_chart("./histograms/combined.png", &combined, "Binary + WASM", 80)?;
    draw_chart("./histograms/combined_wo_dotnet.png", &combined_wo_dotnet, "Binary + WASM w/o .Net", 100)?;
    Ok(())
}