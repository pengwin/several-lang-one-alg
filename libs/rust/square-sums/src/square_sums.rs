use std::{ops::Deref, rc::Rc};

use crate::{
    metrics::Metrics,
    node::Node,
    nodes_sorting_facade::{NodesSortingFacade, NodesSortingWithCacheFacade},
    path::Path,
    tree::{Tree, TreeBuilder},
};

fn is_fair_square(n: u32) -> bool {
    let h = n & 0xF; // h is the last hex "digit"
    if h > 9 {
        return false;
    }

    // Use lazy evaluation to jump out of the if statement as soon as possible
    if h != 2 && h != 3 && h != 5 && h != 6 && h != 7 && h != 8 {
        let t = f64::floor(f64::sqrt(n as f64)) as u32;
        return t * t == n;
    }
    return false;
}

pub fn build_tree(n: u32, sorting: &NodesSortingFacade) -> Result<Option<Tree>, String> {
    let mut builder = TreeBuilder::new(n);

    for i in 1..n + 1 {
        for j in 1..n + 1 {
            if i == j {
                continue;
            }

            let sum = i + j;
            if !is_fair_square(sum) {
                continue;
            }

            builder.add_pair(i, j);
        }
    }

    builder.build(sorting)
}

fn dfs<P>(
    n: u32,
    node: Rc<Node>,
    path: &mut Path,
    metrics: &mut Option<Metrics<P>>,
    sorting: &mut NodesSortingWithCacheFacade,
) -> Result<(), String>
where
    P: Fn(String) -> (),
{
    if let Some(m) = metrics.as_mut() {
        m.increment_dfs_counter();
    }

    let mut pairs_mut = node.pairs_mut()?;

    sorting.sort_nodes(path, &mut pairs_mut);

    let pairs = pairs_mut.deref();

    for p in pairs {
        let node = p.upgrade().ok_or("Item already dropped")?;
        let v = node.value;

        if path.contains(v) {
            continue;
        }

        path.push(v)?;

        if path.count == n {
            break;
        }

        dfs(n, node, path, metrics, sorting)?;
        if path.count == n {
            break;
        }

        path.pop();
    }

    Ok(())
}

pub fn square_sums_row<P>(
    n: u32,
    metrics: &mut Option<Metrics<P>>,
) -> Result<Option<Vec<u32>>, String>
where
    P: Fn(String) -> (),
{
    let sorting = NodesSortingFacade::new();
    match build_tree(n, &sorting)? {
        Some(tree) => {
            for root in &tree.roots {
                let mut path = Path::new(n);
                let node = root.clone();
                let mut sorting = NodesSortingWithCacheFacade::new(n);
                path.push(node.value)?;
                dfs(n, node, &mut path, metrics, &mut sorting)?;
                if path.count == n {
                    if let Some(m) = metrics.as_mut() {
                        m.finalize_dfs_counter(n);
                    }
                    return Ok(Some(path.into()));
                }
            }
            return Ok(None);
        }
        None => Ok(None),
    }
}
