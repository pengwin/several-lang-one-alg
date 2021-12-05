use std::collections::{HashMap, HashSet};
use std::fs;
use std::ops::RangeBounds;

use plotters::style::RGBColor;
use serde::de::DeserializeOwned;
use serde::Deserialize;

#[derive(Debug, Clone)]
pub struct LangDef {
    pub color: RGBColor,
    pub name: String,
    pub duration: f64,

    pub max_mem: f64,

    pub total_heap_diff: f64,
    pub size: f64,
}

fn map_color(key: &str) -> RGBColor {
    let dotnet_color = RGBColor(0, 125, 200);

    if key.to_lowercase().contains("c++") {
        return RGBColor(200, 0, 0);
    }

    if key.to_lowercase().contains("go") {
        return RGBColor(125, 0, 0);
    }

    if key.to_lowercase().contains("js") {
        return RGBColor(0, 200, 0);
    }

    if key.to_lowercase().contains("rust") {
        return RGBColor(0, 125, 125);
    }

    if key.to_lowercase().contains("blazor") {
        return dotnet_color;
    }

    if key.to_lowercase().contains(".net") {
        return dotnet_color;
    }

    panic!("Unknown key {}", key)
}

impl LangDef {
    fn from_binary_metric(key: &str, m: &BinaryMetric) -> LangDef {
        LangDef {
            duration: m.time,
            max_mem: m.max_mem,
            size: 0.0,
            total_heap_diff: 0.0,
            name: key.to_owned(),
            color: map_color(key),
        }
    }

    fn from_wasm_metric(key: &str, m: &WasmMetric) -> LangDef {
        LangDef {
            duration: m.duration,
            max_mem: 0.0,
            size: m.size,
            total_heap_diff: m.total_heap_diff,
            name: key.to_owned(),
            color: map_color(key),
        }
    }
}

#[derive(Debug, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct BinaryMetric {
    pub time: f64,
    pub max_mem: f64,
}

#[derive(Debug, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct WasmMetric {
    pub duration: f64,
    pub total_heap_diff: f64,
    pub size: f64,
}

fn read_metrics<T: DeserializeOwned>(file: &str) -> Result<HashMap<String, T>, String> {
    let file = fs::File::open(file).map_err(|e| format!("Unable to open file {}", e))?;

    let data = serde_json::from_reader(file)
        .map_err(|e| format!("Unable to read json from file {}", e))?;

    Ok(data)
}

pub fn combine(a: &[LangDef], b: &[LangDef]) -> Vec<LangDef> {
    let mut res = vec![];

    // use to get desired order
    let names = ["c++", "rust", "go", "js", ".net", "blazor"];

    for n in names {
        for i in a.iter().filter(|e| e.name.to_lowercase().contains(n)) {
            res.push(i.clone());
        }
        for i in b.iter().filter(|e| e.name.to_lowercase().contains(n)) {
            res.push(i.clone());
        }
    }

    res
}

fn test_name(a: &LangDef, test_list: &[&str]) -> bool {
    for n in test_list {
        if a.name.to_lowercase().contains(n) {
            return true
        }
    }

    false
}

pub fn exclude(a: &[LangDef], exclude_list: &[&str]) -> Vec<LangDef> {
    a.into_iter()
        .filter(|a| !test_name(a, exclude_list))
        .map(|l| l.clone())
        .collect()
}

fn read_map_metrics<T: DeserializeOwned, M: Fn(&str, &T) -> LangDef>(file: &str, mapper_fn: M) -> Result<Vec<LangDef>, String> {
    let map = read_metrics::<T>(file)?;

    let langs = ["c++", "rust", "go", "js", ".net", "blazor"];

    let mut res = vec![];

    for l in langs {
        for (k ,v) in &map {
            if k.to_lowercase().contains(&l) {
                res.push(mapper_fn(k, v));
            }
        }
    }

    Ok(res)
}

pub fn read_binary_metrics(file: &str) -> Result<Vec<LangDef>, String> {
    read_map_metrics::<BinaryMetric, _>(file, LangDef::from_binary_metric)
}

pub fn read_wasm_metrics(file: &str) -> Result<Vec<LangDef>, String> {
    read_map_metrics::<WasmMetric, _>(file, LangDef::from_wasm_metric)
}
