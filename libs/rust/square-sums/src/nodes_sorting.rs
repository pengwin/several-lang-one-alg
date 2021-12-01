use std::{
    cell::RefMut,
    cmp::Ordering,
    rc::{Rc, Weak}, ops::Deref,
};

use crate::{node::Node, path::Path};

fn pairs_not_in_path(node: &Rc<Node>, path: &Path) -> Result<u32, String> {
    let mut count = 0;
    let pair_values_ref = node.pair_values()?;
    let pair_values = pair_values_ref.deref();
    for v in pair_values {
        if path.contains(*v) {
            continue;
        }
        count += 1;
    }
    return Ok(count);
}

struct PairsNotInPathCache {
    cache: Vec<Option<u32>>,
}

impl PairsNotInPathCache {
    pub fn new(n: u32) -> PairsNotInPathCache {
        PairsNotInPathCache {
            cache: vec![None; (n + 1) as usize],
        }
    }

    pub fn flush(&mut self) {
        for i in 0..self.cache.len() {
            self.cache[i] = None
        }
    }

    pub fn get_pairs_not_in_path(&mut self, path: &Path, node: &Rc<Node>) -> Result<u32, String> {
        let index = node.value as usize;
        let value = self.cache[index];
        if let Some(result) = value {
            return Ok(result);
        }

        let pairs = pairs_not_in_path(node, path)?;
        self.cache[index] = Some(pairs);

        return Ok(pairs);
    }
}

fn nodes_comparer_simple(i: &Rc<Node>, j: &Rc<Node>) -> Result<Ordering, String> {
    {
        let a = i.pairs_count()?;
        let b = j.pairs_count()?;

        if a != b {
            return Ok(a.cmp(&b));
        }
    }

    let a = i.value;
    let b = j.value;

    if a < b {
        return Ok(Ordering::Greater);
    }

    if a > b {
        return Ok(Ordering::Less);
    }

    Ok(Ordering::Equal)
}

fn nodes_comparer_with_path(
    cache: &mut PairsNotInPathCache,
    path: &Path,
    i: &Weak<Node>,
    j: &Weak<Node>,
) -> Result<Ordering, String> {
    let i_ref = i
        .upgrade()
        .expect("Received unexpected null or deallocated node");
    let j_ref = j
        .upgrade()
        .expect("Received unexpected null or deallocated node");

    let a = cache.get_pairs_not_in_path(path, &i_ref)?;
    let b = cache.get_pairs_not_in_path(path, &j_ref)?;

    if a != b {
        return Ok(a.cmp(&b));
    }

    nodes_comparer_simple(&i_ref, &j_ref)
}

pub struct NodesSorting {}

impl NodesSorting {
    pub fn new() -> NodesSorting {
        NodesSorting {}
    }

    pub fn sort_nodes(&self, nodes: &mut Vec<Rc<Node>>) {
        nodes.sort_unstable_by(|i, j| nodes_comparer_simple(i, j).expect(""));
    }
}

pub struct NodesSortingWithCache {
    cache: PairsNotInPathCache,
}

impl NodesSortingWithCache {
    pub fn new(n: u32) -> NodesSortingWithCache {
        NodesSortingWithCache {
            cache: PairsNotInPathCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut RefMut<Vec<Weak<Node>>>) {
        self.cache.flush();
        nodes.sort_unstable_by(|i, j| nodes_comparer_with_path(&mut self.cache, path, i, j).expect(""));
    }
}
