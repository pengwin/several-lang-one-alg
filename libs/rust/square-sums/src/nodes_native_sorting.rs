use std::{
    cmp::Ordering,
    rc::{Rc, Weak},
};

use crate::{
    node::Node,
    nodes_compare::{nodes_comparer, nodes_comparer_with_path},
    pairs_not_in_path_cache::PairsNotInPathCache,
    path::Path,
};

#[inline(always)]
#[cfg(not(feature = "stablesort"))]
fn sort_facade<F, T>(nodes: &mut [T], compare: F)
where
    F: FnMut(&T, &T) -> Ordering,
{
    nodes.sort_unstable_by(compare);
}

#[inline(always)]
#[cfg(feature = "stablesort")]
fn sort_facade<F, T>(nodes: &mut [T], compare: F)
where
    F: FnMut(&T, &T) -> Ordering,
{
    nodes.sort_by(compare);
}

#[cfg(not(feature = "qsort"))]
pub struct NativeNodesSorting {}

#[cfg(not(feature = "qsort"))]
impl NativeNodesSorting {
    pub fn new() -> NativeNodesSorting {
        NativeNodesSorting {}
    }

    pub fn sort_nodes(&self, nodes: &mut [Rc<Node>]) {
        sort_facade(nodes, |i, j| nodes_comparer(i, j).expect(""));
    }
}

#[cfg(not(feature = "qsort"))]
pub struct NativeNodesSortingWithCache {
    cache: PairsNotInPathCache,
}

#[cfg(not(feature = "qsort"))]
impl NativeNodesSortingWithCache {
    pub fn new(n: u32) -> NativeNodesSortingWithCache {
        NativeNodesSortingWithCache {
            cache: PairsNotInPathCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut [Weak<Node>]) {
        self.cache.flush();
        sort_facade(nodes, |i, j| {
            nodes_comparer_with_path(&mut self.cache, path, i, j).expect("")
        });
    }
}
