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
//what is even nicer is that AI picked on this from implication in the comment, not prompted to do so!
// Pi Monte Carlo Single Thread:
// For Single Thread test size: 50000000 Pi estimate is: 3.14142832 and took 396.94 ms with peak memory: 15597568 KB
// For Single Thread test size: 500000000 Pi estimate is: 3.141519192 and took 4003.15 ms with peak memory: 16031744 KB
// For Single Thread test size: 2000000000 Pi estimate is: 3.141549136 and took 15859.16 ms with peak memory: 16101376 KB

// Pi Monte Carlo Multi Threaded:
// For Multi Threaded test size: 50000000 Pi estimate is: 3.14139632 and took 89.14 ms with peak memory: 16134144 KB
// For Multi Threaded test size: 500000000 Pi estimate is: 3.141691592 and took 763.50 ms with peak memory: 16228352 KB
// For Multi Threaded test size: 2000000000 Pi estimate is: 3.14160043 and took 3095.56 ms with peak memory: 16445440 KB




//cargo run          slooooooooooooooooowwwwwwwwwwwwwwwwwwwwww
//Pi Monte Carlo Single Thread:
//For Single Thread test size: 50000000 Pi estimate is: 3.14162888 and took 37271.00 miliseconds
//Pi Monte Carlo Multi Threaded:
//For Multi Threaded test size: 50000000 Pi estimate is: 3.1414148 and took 6694.05 ms