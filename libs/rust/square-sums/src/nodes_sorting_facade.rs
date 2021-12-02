use std::rc::{Rc, Weak};

use crate::{node::Node, path::Path};

//use crate::nodes_native_sorting::{NativeNodesSorting, NativeNodesSortingWithCache};
use crate::nodes_qsort_sorting::{QSortNodesSorting, QSortNodesSortingWithCache};
pub struct NodesSortingFacade {
    sorting: QSortNodesSorting,
}

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

pub struct NodesSortingWithCacheFacade {
    sorting: QSortNodesSortingWithCache,
}

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
