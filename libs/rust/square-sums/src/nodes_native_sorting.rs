use std::rc::{Rc, Weak};

use crate::{
    node::Node,
    nodes_compare::{nodes_comparer, nodes_comparer_with_path},
    pairs_not_in_path_cache::PairsNotInPathCache,
    path::Path,
};

pub struct NativeNodesSorting {}

impl NativeNodesSorting {
    pub fn new() -> NativeNodesSorting {
        NativeNodesSorting {}
    }

    pub fn sort_nodes(&self, nodes: &mut [Rc<Node>]) {
        nodes.sort_unstable_by(|i, j| nodes_comparer(i, j).expect(""));
    }
}

pub struct NativeNodesSortingWithCache {
    cache: PairsNotInPathCache,
}

impl NativeNodesSortingWithCache {
    pub fn new(n: u32) -> NativeNodesSortingWithCache {
        NativeNodesSortingWithCache {
            cache: PairsNotInPathCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut [Weak<Node>]) {
        self.cache.flush();
        nodes.sort_unstable_by(|i, j| {
            nodes_comparer_with_path(&mut self.cache, path, i, j).expect("")
        });
    }
}
