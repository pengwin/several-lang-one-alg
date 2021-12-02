use std::{ops::Deref, rc::Rc};

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

pub struct PairsNotInPathCache {
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
