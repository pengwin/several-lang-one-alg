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

#[cfg(feature = "qsort")]
fn partition<N, C: FnMut(&N, &N) -> Ordering>(
    comparer: &mut C,
    nodes: &mut [N],
    start: usize,
    end: usize,
) -> usize {
    let mut marker = start;
    for i in start..end {
        let compare_result = comparer(&nodes[i], &nodes[end]);
        if compare_result == Ordering::Less {
            nodes.swap(i, marker);
            marker += 1;
        }
    }

    nodes.swap(marker, end);
    marker
}

#[cfg(feature = "qsort")]
fn quicksort<N, C: FnMut(&N, &N) -> Ordering>(mut comparer: C, nodes: &mut [N]) {
    quicksort_recursive(&mut comparer, nodes, 0, nodes.len() - 1)
}

#[cfg(feature = "qsort")]
fn quicksort_recursive<N, C: FnMut(&N, &N) -> Ordering>(
    comparer: &mut C,
    nodes: &mut [N],
    start: usize,
    end: usize,
) {
    if start >= end {
        return;
    }

    let pivot = partition(comparer, nodes, start, end);
    if pivot > 0 {
        quicksort_recursive(comparer, nodes, start, pivot - 1);
    }
    quicksort_recursive(comparer, nodes, pivot + 1, end);
}

#[cfg(feature = "qsort")]
pub struct QSortNodesSorting {}

#[cfg(feature = "qsort")]
impl QSortNodesSorting {
    pub fn new() -> QSortNodesSorting {
        QSortNodesSorting {}
    }

    pub fn sort_nodes(&self, nodes: &mut [Rc<Node>]) {
        quicksort(|i, j| nodes_comparer(i, j).expect(""), nodes);
    }
}

#[cfg(feature = "qsort")]
pub struct QSortNodesSortingWithCache {
    cache: PairsNotInPathCache,
}

#[cfg(feature = "qsort")]
impl QSortNodesSortingWithCache {
    pub fn new(n: u32) -> QSortNodesSortingWithCache {
        QSortNodesSortingWithCache {
            cache: PairsNotInPathCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut [Weak<Node>]) {
        self.cache.flush();
        quicksort(
            |i, j| nodes_comparer_with_path(&mut self.cache, path, i, j).expect(""),
            nodes,
        );
    }
}
