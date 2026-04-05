mod simple_threadable_monte_carlo_pi;

use simple_threadable_monte_carlo_pi::SimpleMonteCarlo;

fn main() {
    println!("Pi Monte Carlo Single Thread:");
    let mc = SimpleMonteCarlo::new();
    println!("{}", mc.run_single_thread());

    println!("Pi Monte Carlo Multi Threaded:");
    let mc = SimpleMonteCarlo::new();
    println!("{}", mc.run_multi_threaded());
    
}

//cargo run --release  -  NICE! :)
// Pi Monte Carlo Single Thread:
// For Single Thread test size: 50000000 Pi estimate is: 3.1409632 and took 397.82 miliseconds

// Pi Monte Carlo Multi Threaded:
// For Multi Threaded test size: 50000000 Pi estimate is: 3.1420132 and took 83.66 ms

//cargo run          slooooooooooooooooowwwwwwwwwwwwwwwwwwwwww
//Pi Monte Carlo Single Thread:
//For Single Thread test size: 50000000 Pi estimate is: 3.14162888 and took 37271.00 miliseconds
//Pi Monte Carlo Multi Threaded:
//For Multi Threaded test size: 50000000 Pi estimate is: 3.1414148 and took 6694.05 ms