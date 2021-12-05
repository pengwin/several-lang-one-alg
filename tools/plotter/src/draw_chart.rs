use std::{collections::HashMap, path::PathBuf};

use plotters::prelude::*;

use crate::models::LangDef;

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

fn unwrap_str(s: &SegmentValue<&String>) -> String {
    match s {
        SegmentValue::Exact(s) => s.to_string(),
        SegmentValue::CenterOf(s) => s.to_string(),
        SegmentValue::Last => panic!("Unexpected last"),
    }
}

pub fn draw_chart<E: Fn(&LangDef) -> f64>(
    file_path: &PathBuf,
    langs: &[LangDef],
    caption: &str,
    y_desc: &str,
    margin: u32,
    extractor_fn: E
) -> Result<(), Box<dyn std::error::Error>> {
    let root = BitMapBackend::new(file_path, (1024, 480)).into_drawing_area();

    let max = calc_max(langs);

    let x_spec = build_x_spec(langs);
    println!("{:?}", x_spec);

    root.fill(&WHITE)?;

    let mut chart = ChartBuilder::on(&root)
        .x_label_area_size(35)
        .y_label_area_size(50)
        .margin(5)
        .caption(caption, ("sans-serif", 50.0))
        .build_cartesian_2d(x_spec.into_segmented(), 0f64..max)?;

    chart
        .configure_mesh()
        .disable_x_mesh()
        .bold_line_style(&WHITE.mix(0.3))
        .y_desc(y_desc)
        .x_desc("Langs")
        .x_labels(100)
        .axis_desc_style(("sans-serif", 15))
        .draw()?;

    let mut map = HashMap::new();
    for f in langs {
        map.insert(&f.name, f.clone());
    }

    chart.draw_series(
        Histogram::vertical(&chart)
            .margin(margin)
            .style_func(|lang, _| {
                let def = map.get(&unwrap_str(lang)).expect("To have it in map");
                def.color.mix(0.9).filled()
            })
            .data(
                x_spec
                    .iter()
                    .map(|lang| (lang, extractor_fn(map.get(lang).expect("To have it in map")))),
            ),
    )?;

    // To avoid the IO failure being ignored silently, we manually call the present function
    root.present().expect("Unable to write result to file, please make sure 'plotters-doc-data' dir exists under current dir");
    println!(
        "Result has been saved to {}",
        file_path.to_str().expect("Unable to get string")
    );

    Ok(())
}
