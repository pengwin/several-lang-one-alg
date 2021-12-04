pub struct Path {
    pub count: u32,
    items: Vec<u32>,
    attached: Vec<bool>,
}

impl Path {
    pub fn new(n: u32) -> Path {
        let size = n as usize +1;
        Path { 
            count: 0, 
            items: vec![0; size],
            attached: vec![false; size]
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

        let items_pos = self.count as usize;
        self.items[items_pos] = n;
        self.count += 1;
        self.attached[index] = true;

        Ok(())
    }

    pub fn pop(&mut self) {
        if self.count == 0 {
            return;
        }

        let current_pos = self.count as usize - 1;
        let last = self.items[current_pos];
        self.items[current_pos] = 0;

        let index = last as usize;
        self.attached[index] = false;
        self.count -= 1;
    }

    pub fn items(&self) -> Vec<u32> {
        let size = self.count as usize;
        let slice = &self.items[0..size];
        let mut result = vec![];
        result.extend_from_slice(slice);
        result
    }
}