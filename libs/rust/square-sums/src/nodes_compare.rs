use std::{
    cmp::Ordering,
    rc::{Rc, Weak},
};

use crate::{node::Node, pairs_not_in_path_cache::PairsNotInPathCache, path::Path};

pub fn nodes_comparer(i: &Rc<Node>, j: &Rc<Node>) -> Result<Ordering, String> {
    {
        let a = i.pairs_count()?;
        let b = j.pairs_count()?;

        if a != b {
            return Ok(a.cmp(&b));
        }
    }

    let a = i.value;
    let b = j.value;

    if a < b {
        return Ok(Ordering::Greater);
    }

    if a > b {
        return Ok(Ordering::Less);
    }

    Ok(Ordering::Equal)
}

pub fn nodes_comparer_with_path(
    cache: &mut PairsNotInPathCache,
    path: &Path,
    i: &Weak<Node>,
    j: &Weak<Node>,
) -> Result<Ordering, String> {
    let i_ref = i
        .upgrade()
        .expect("Received unexpected null or deallocated node");
    let j_ref = j
        .upgrade()
        .expect("Received unexpected null or deallocated node");

    let a = cache.get_pairs_not_in_path(path, &i_ref)?;
    let b = cache.get_pairs_not_in_path(path, &j_ref)?;

    if a != b {
        return Ok(a.cmp(&b));
    }

    nodes_comparer(&i_ref, &j_ref)
}


