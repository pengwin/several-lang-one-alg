use std::{rc::Rc};

use crate::{node::Node, nodes_sorting_trait::NodesSorting};

pub struct Tree {
    pub roots: Vec<Rc<Node>>
}

impl Tree {
    pub fn new(roots: Vec<Rc<Node>>) -> Tree {
        Tree{roots}
    }
}

pub struct TreeBuilder {
    nodes: Vec<Option<Rc<Node>>>
}

impl TreeBuilder {

    pub fn new(n: u32) -> TreeBuilder {
        TreeBuilder{
            nodes: vec![None; (n+1)
             as usize]
        }
    }

    pub fn add_pair(&mut self, head: u32, tail: u32) {
        let head_node = self.get_or_create(head);
        let tail_node = self.get_or_create(tail);
        head_node.add(Rc::downgrade(&tail_node));
    }

    pub fn build<S: NodesSorting>(&mut self, sorting: &S) -> Result<Option<Tree>, String> {
        let size = self.nodes.len();
        let mut nodes = Vec::with_capacity(size);

        for index in 1..size {
            let builder_node = &self.nodes[index];

            match builder_node {
                Some(existing_node) => {
                    existing_node.finalize()?;
                    nodes.push(existing_node.clone())
                },
                None => return Ok(None),
            }

        }

        sorting.sort_nodes(&mut nodes);
        
        Ok(Some(Tree::new(nodes)))
    }

    fn get_or_create(&mut self, n: u32) -> Rc<Node> {
        let index = n as usize;
        match &self.nodes[index] {
            Some(node) => node.clone(),
            None => {
                let node = Rc::new(Node::new(n));
                self.nodes[index] = Some(node.clone());
                node
            },
        }
    }
}