use std::{
    cell::{Ref, RefCell, RefMut},
    ops::Deref,
    rc::{Weak},
};

pub struct Node {
    pairs: RefCell<Vec<Weak<Node>>>,
    pairs_values: RefCell<Vec<u32>>,
    count: RefCell<usize>,
    pub value: u32,
}

impl Node {
    pub fn new(val: u32) -> Node {
        Node {
            pairs: RefCell::new(vec![]),
            pairs_values: RefCell::new(vec![]),
            count: RefCell::new(0),
            value: val,
        }
    }

    pub fn add(&self, node: Weak<Node>) {
        let mut pairs_mut = self.pairs.borrow_mut();
        pairs_mut.push(node);
    }

    pub fn pairs_mut(&self) -> Result<RefMut<Vec<Weak<Node>>>, String> {
        let mut_ref = self.pairs.try_borrow_mut().map_err(|_| {
            format!(
                "BorrowMutError. Pairs of node {} already borrowed",
                self.value
            )
        })?;
        Ok(mut_ref)
    }

    pub fn pair_values(&self) -> Result<Ref<Vec<u32>>, String> {
        let mut_ref = self.pairs_values.try_borrow().map_err(|_| {
            format!(
                "BorrowError. Pair values of node {} already borrowed",
                self.value
            )
        })?;
        Ok(mut_ref)
    }

    pub fn pairs_count(&self) -> Result<usize, String> {
        let count_ref = self.count.try_borrow().map_err(|_| {
            format!(
                "BorrowError. Pairs count of node {} already borrowed",
                self.value
            )
        })?;
        let value = *count_ref.deref();
        Ok(value)
    }

    pub fn finalize(&self) -> Result<(), String> {
        let pairs = self
            .pairs
            .try_borrow()
            .map_err(|_| format!("BorrowError. Pairs of node {} already borrowed", self.value))?;
        let count = pairs.len();
        self.count.replace(count);

        let mut pair_values = vec![0; count];
        for i in 0..count {
            let p = &pairs[i];
            let pair = p
                .upgrade()
                .ok_or("Received unexpected null or deallocated node")?;

            pair_values[i] = pair.value;
        }
        self.pairs_values.replace(pair_values);
        Ok(())
    }
}
