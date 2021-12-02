use std::{rc::{Rc, Weak}, cell::RefMut};

use crate::{node::Node, path::Path};

pub trait NodesSorting {
    fn new() -> Self;

    fn sort_nodes(&self, nodes: &mut Vec<Rc<Node>>);
}
pub trait NodesSortingWithCache {
    fn new(n: u32) -> Self;

    fn sort_nodes(&mut self, path: &Path, nodes: &mut RefMut<Vec<Weak<Node>>>);
}