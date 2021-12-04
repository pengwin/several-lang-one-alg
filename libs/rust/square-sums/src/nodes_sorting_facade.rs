use std::rc::{Rc, Weak};

use crate::{node::Node, path::Path};

#[cfg(not(feature = "qsort"))]
use crate::nodes_native_sorting::{NativeNodesSorting, NativeNodesSortingWithCache};
#[cfg(feature = "qsort")]
use crate::nodes_qsort_sorting::{QSortNodesSorting, QSortNodesSortingWithCache};


#[cfg(feature = "qsort")]
pub struct NodesSortingFacade {
    sorting: QSortNodesSorting,
}

#[cfg(feature = "qsort")]
impl NodesSortingFacade {
    pub fn new() -> NodesSortingFacade {
        NodesSortingFacade {
            sorting: QSortNodesSorting::new(),
        }
    }

    pub fn sort_nodes(&self, nodes: &mut [Rc<Node>]) {
        self.sorting.sort_nodes(nodes)
    }
}

#[cfg(feature = "qsort")]
pub struct NodesSortingWithCacheFacade {
    sorting: QSortNodesSortingWithCache,
}

#[cfg(feature = "qsort")]
impl NodesSortingWithCacheFacade {
    pub fn new(n: u32) -> NodesSortingWithCacheFacade {
        NodesSortingWithCacheFacade {
            sorting: QSortNodesSortingWithCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut [Weak<Node>]) {
        self.sorting.sort_nodes(path, nodes)
    }
}

#[cfg(not(feature = "qsort"))]
pub struct NodesSortingFacade {
    sorting: NativeNodesSorting,
}

#[cfg(not(feature = "qsort"))]
impl NodesSortingFacade {
    pub fn new() -> NodesSortingFacade {
        NodesSortingFacade {
            sorting: NativeNodesSorting::new(),
        }
    }

    pub fn sort_nodes(&self, nodes: &mut [Rc<Node>]) {
        self.sorting.sort_nodes(nodes)
    }
}

#[cfg(not(feature = "qsort"))]
pub struct NodesSortingWithCacheFacade {
    sorting: NativeNodesSortingWithCache,
}

#[cfg(not(feature = "qsort"))]
impl NodesSortingWithCacheFacade {
    pub fn new(n: u32) -> NodesSortingWithCacheFacade {
        NodesSortingWithCacheFacade {
            sorting: NativeNodesSortingWithCache::new(n),
        }
    }

    pub fn sort_nodes(&mut self, path: &Path, nodes: &mut [Weak<Node>]) {
        self.sorting.sort_nodes(path, nodes)
    }
}
