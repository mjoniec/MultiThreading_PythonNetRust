use rand::Rng;
use rayon::prelude::*;
use std::time::Instant;
use sysinfo::{System, Pid};

pub struct HardMergeSort {
    test_sizes: [usize; 3],
    threshold: usize,
}

impl HardMergeSort {
    pub fn new() -> Self {
        Self {
            test_sizes: [1_000_000, 10_000_000, 100_000_000],
            threshold: 2048,
        }
    }

    pub fn run_single_thread_tests(&self) -> String {
        let mut results = String::new();
        for &size in &self.test_sizes {
            let mut data = self.get_random_table(size);
            results.push_str(&self.timer_text("Single Thread", &mut data, false));
            results.push('\n');
        }
        results
    }

    pub fn run_multi_thread_tests(&self) -> String {
        let mut results = String::new();
        for &size in &self.test_sizes {
            let mut data = self.get_random_table(size);
            results.push_str(&self.timer_text("Multi Thread", &mut data, true));
            results.push('\n');
        }
        results
    }

    fn timer_text(&self, mode: &str, data: &mut [i32], parallel: bool) -> String {
        let mut sys = System::new_all();
        let start = Instant::now();

        if parallel {
            self.merge_sort_parallel(data);
        } else {
            self.merge_sort(data);
        }

        let elapsed = start.elapsed();
        sys.refresh_processes(sysinfo::ProcessesToUpdate::All, true);
        let memory = sys.process(sysinfo::get_current_pid().unwrap())
            .map(|p| p.memory())
            .unwrap_or(0);

        format!(
            "For {} test size: {} sorting took: {:.2} ms with memory: {} KB",
            mode, data.len(), elapsed.as_secs_f64() * 1000.0, memory
        )
    }

    fn merge_sort(&self, slice: &mut [i32]) {
        let len = slice.len();
        if len <= 1 { return; }

        let mid = len / 2;
        let (left, right) = slice.split_at_mut(mid);

        self.merge_sort(left);
        self.merge_sort(right);
        self.merge(slice, mid);
    }

    fn merge_sort_parallel(&self, slice: &mut [i32]) {
        let len = slice.len();
        if len < self.threshold {
            self.merge_sort(slice);
            return;
        }

        let mid = len / 2;
        let (left, right) = slice.split_at_mut(mid);

        // Rayon join zastępuje Parallel.Invoke
        rayon::join(
            || self.merge_sort_parallel(left),
            || self.merge_sort_parallel(right),
        );

        self.merge(slice, mid);
    }

    fn merge(&self, slice: &mut [i32], mid: usize) {
        let mut temp = Vec::with_capacity(slice.len());
        let (left, right) = slice.split_at(mid);
        
        let (mut i, mut j) = (0, 0);

        while i < left.len() && j < right.len() {
            if left[i] <= right[j] {
                temp.push(left[i]);
                i += 1;
            } else {
                temp.push(right[j]);
                j += 1;
            }
        }

        temp.extend_from_slice(&left[i..]);
        temp.extend_from_slice(&right[j..]);
        slice.copy_from_slice(&temp);
    }

    fn get_random_table(&self, n: usize) -> Vec<i32> {
        let mut rng = rand::thread_rng();
        (0..n).map(|_| rng.r#gen_range(1..i32::MAX)).collect()
    }
}