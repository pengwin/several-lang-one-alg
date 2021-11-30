use std::rc::Rc;
use std::convert::Into;

struct PathNode {
    pub prev: Option<Rc<PathNode>>,
    pub value: u32,
}

impl PathNode {
    pub fn new(n: u32, prev: Option<Rc<PathNode>>) -> PathNode{
        PathNode{value: n, prev}
    }
}

pub struct Path {
    pub count: u32,
    last: Option<Rc<PathNode>>,
    attached: Vec<bool>,
}

impl Path {
    pub fn new(n: u32) -> Path {
        Path { 
            count: 0, 
            last: None, 
            attached: vec![false; n as usize +1]
        }
    }

    pub fn contains(&self, n: u32) -> bool {
        return self.attached[n as usize];
    }

    pub fn push(&mut self, n: u32) -> Result<(), String> {
        let index = n as usize;
        if self.attached[index] {
            return Err(format!("{} already attached", n))
        }

        let prev = self.last.clone();
        self.last = Some(Rc::new(PathNode::new(n, prev)));
        self.count += 1;
        self.attached[index] = true;

        Ok(())
    }

    pub fn pop(&mut self) {
        match &self.last {
            Some(last) => {
                let index = last.value as usize;
                self.attached[index] = false;
                let prev = last.prev.clone();
                self.last = prev;
                self.count -= 1;
            },
            None => return
        }
    }
}

fn path_to_vector(node: Option<Rc<PathNode>>, v: &mut Vec<u32>) {
    let mut prev = node;
    while let Some(node) = prev {
        v.push(node.value);
        prev = node.prev.clone();
    }
}

impl Into<Vec<u32>> for Path {

    fn into(self) -> Vec<u32> {
        let mut v = Vec::new();
        path_to_vector(self.last.clone(), &mut v);
        v
    }
}