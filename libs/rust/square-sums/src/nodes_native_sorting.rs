use std::{
    cell::RefMut,
    rc::{Rc, Weak},
};

use crate::nodes_sorting_trait::{NodesSorting, NodesSortingWithCache};

use crate::{
    node::Node,
    nodes_compare::{nodes_comparer, nodes_comparer_with_path},
    pairs_not_in_path_cache::PairsNotInPathCache,
    path::Path,
};

pub struct NativeNodesSorting {}

impl NodesSorting for NativeNodesSorting {
    fn new() -> NativeNodesSorting {
        NativeNodesSorting {}
    }

    fn sort_nodes(&self, nodes: &mut Vec<Rc<Node>>) {
        nodes.sort_unstable_by(|i, j| nodes_comparer(i, j).expect(""));
    }
}

pub struct NativeNodesSortingWithCache {
    cache: PairsNotInPathCache,
}

impl NodesSortingWithCache for NativeNodesSortingWithCache {
    fn new(n: u32) -> NativeNodesSortingWithCache {
        NativeNodesSortingWithCache {
            cache: PairsNotInPathCache::new(n),
        }
    }

    fn sort_nodes(&mut self, path: &Path, nodes: &mut RefMut<Vec<Weak<Node>>>) {
        self.cache.flush();
        nodes.sort_unstable_by(|i, j| {
            nodes_comparer_with_path(&mut self.cache, path, i, j).expect("")
        });
    }
}
