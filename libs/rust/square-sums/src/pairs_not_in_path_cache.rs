use std::rc::Rc;

use crate::{node::Node, path::Path};

fn count_pairs_not_in_path(node: &Rc<Node>, path: &Path) -> Result<u32, String> {
    let mut count = 0;
    let pair_values = node.pair_values()?;
    let values_size = pair_values.len();
    for index in 0..values_size {
        if path.contains(pair_values[index]) {
            continue;
        }
        count += 1;
    }
    return Ok(count);
}

pub struct PairsNotInPathCache {
    cache: Vec<Option<u32>>,
    max_cache_position: usize,
}

impl PairsNotInPathCache {
    pub fn new(n: u32) -> PairsNotInPathCache {
        PairsNotInPathCache {
            cache: vec![None; (n + 1) as usize],
            max_cache_position: 0,
        }
    }

    pub fn flush(&mut self) {
        if self.max_cache_position == 0 {
            return;
        }
        self.cache.fill(None);
        self.max_cache_position = 0;
    }

    pub fn get_pairs_not_in_path(&mut self, path: &Path, node: &Rc<Node>) -> Result<u32, String> {
        let index = node.value as usize;
        let value = self.cache[index];
        if let Some(result) = value {
            return Ok(result);
        }

        let pairs = count_pairs_not_in_path(node, path)?;
        self.cache[index] = Some(pairs);
        if index > self.max_cache_position {
            self.max_cache_position = index;
        }

        return Ok(pairs);
    }
}
