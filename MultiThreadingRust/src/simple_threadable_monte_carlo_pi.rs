use rand::Rng;
use rayon::prelude::*;
use std::time::Instant;
use rand::thread_rng;
use sysinfo::{System, Pid};

pub struct SimpleMonteCarlo {
    //tests: [u64; 1],
    tests: [u64; 3],
}

impl SimpleMonteCarlo {
    pub fn new() -> Self {
        Self {
            //tests: [50_000_000],
            tests: [50_000_000, 500_000_000, 2_000_000_000],
        }
    }

    pub fn run_single_thread(&self) -> String {
        let mut results = String::new();
        for &size in &self.tests {
            results.push_str(&self.run_loop(size));
            results.push('\n');
        }
        results
    }

    fn run_loop(&self, number_of_points: u64) -> String {
        let mut sys = System::new_all();
        let start_time = Instant::now();
        let mut points_inside_circle = 0u64;
        let mut rng = rand::thread_rng();

        for _ in 0..number_of_points {
            let x: f64 = rng.r#gen(); // Generuje zakres [0.0, 1.0)
            let y: f64 = rng.r#gen();
            if x * x + y * y <= 1.0 {
                points_inside_circle += 1;
            }
        }

        self.format_output("Single Thread", number_of_points, points_inside_circle, start_time, &mut sys)
    }

    pub fn run_multi_threaded(&self) -> String {
        let mut results = String::new();
        for &size in &self.tests {
            results.push_str(&self.run_loop_multi(size));
            results.push('\n');
        }
        results
    }

    fn run_loop_multi(&self, number_of_points: u64) -> String {
        let mut sys = System::new_all();
        let start = Instant::now();

        // Rayon automatycznie zarzadza pula watków i dzieli zadania
        let points_inside_circle: u64 = (0..number_of_points)
            .into_par_iter() // Tworzy równolegly iterator
            .map_init(
                || thread_rng(), // Kazdy watek dostaje wlasny generator (bardzo wydajne)
                |rng, _index| {
                    let x: f64 = rng.r#gen();
                    let y: f64 = rng.r#gen();
                    if x * x + y * y <= 1.0 { 1 } else { 0 }
                },
            )
            .sum();

        self.format_output("Multi Threaded", number_of_points, points_inside_circle, start, &mut sys)
    }

    fn format_output(&self, mode: &str, size: u64, inside: u64, start: Instant, sys: &mut System) -> String {
        let pi_estimate = 4.0 * (inside as f64) / (size as f64);
        let elapsed = start.elapsed();
        
        //peak memory usage
        sys.refresh_processes(sysinfo::ProcessesToUpdate::All, true); // Odświeżamy dane procesów
        let pid = sysinfo::get_current_pid().unwrap();
        let peak_memory = sys.process(pid) // Pobieramy aktualne użycie pamięci (RSS) w KB
            .map(|p| p.memory()) 
            .unwrap_or(0);

        format!(
            "For {} test size: {} Pi estimate is: {} and took {:.2} ms with peak memory: {} KB",
            mode, size, pi_estimate, elapsed.as_secs_f64() * 1000.0, peak_memory
        )
    }
}
