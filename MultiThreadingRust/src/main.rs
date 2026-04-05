mod simple_threadable_monte_carlo_pi;

use simple_threadable_monte_carlo_pi::SimpleMonteCarlo;

fn main() {
    println!("Pi Monte Carlo Single Thread:");
    let mc = SimpleMonteCarlo::new();
    println!("{}", mc.run_single_thread());
}

//Pi Monte Carlo Single Thread:
//For Single Thread test size: 50000000 Pi estimate is: 3.14162888 and took 37271.00 miliseconds
