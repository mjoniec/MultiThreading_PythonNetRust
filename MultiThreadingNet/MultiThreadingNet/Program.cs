namespace MultiThreadingNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pi Monte Carlo Single Thread:");
            Console.WriteLine(new SimpleThreadableMonteCarloPi().RunSingleThread());
            Console.WriteLine("Pi Monte Carlo Multi Threaded:");
            Console.WriteLine(new SimpleThreadableMonteCarloPi().RunMultiThreaded());
            Console.WriteLine("Merge Sort Single Thread:");
            Console.WriteLine(new HardThreadableMergeSort().RunMergeSortSingleThreadTests());
            Console.WriteLine("Merge Sort Multi Threaded:");
            Console.WriteLine(new HardThreadableMergeSort().RunMergeSortMultiThreadedTests());
        }
    }
}

/* 

Pi Monte Carlo Single Thread:
For Single Thread test size:   50000000 Pi estimate is:  3.14153608 and took  1008.8628 miliseconds to calculate with peak memory at: 35618816
For Single Thread test size:  500000000 Pi estimate is:  3.14152424 and took  7520.3479 miliseconds to calculate with peak memory at: 37470208
For Single Thread test size: 2000000000 Pi estimate is: 3.141553622 and took 29547.1995 miliseconds to calculate with peak memory at: 37707776

Pi Monte Carlo Multi Threaded:
For Multi Threaded test size:   50000000 Pi estimate is: 3.14168872  and took  525.9743 miliseconds to calculate with peak memory at: 35360768
For Multi Threaded test size:  500000000 Pi estimate is: 3.141532776 and took 2543.7616 miliseconds to calculate with peak memory at: 36601856
For Multi Threaded test size: 2000000000 Pi estimate is: 3.141625438 and took 9615.4509 miliseconds to calculate with peak memory at: 38166528

Merge Sort Single Thread::
For Single Thread test size:   1000000 sorting took:   452.6238 miliseconds with peak memory at:   54 886 400
For Single Thread test size:  10000000 sorting took:  3604.2488 miliseconds with peak memory at:  294 260 736
For Single Thread test size: 100000000 sorting took: 39651.9805 miliseconds with peak memory at: 1740 173 312

Merge Sort Multi Threaded:
For Multi Thread test size:   1000000 sorting took:   166.0000 miliseconds with peak memory at: 1740 173 312
For Multi Thread test size:  10000000 sorting took:   978.5661 miliseconds with peak memory at: 1740 173 312
For Multi Thread test size: 100000000 sorting took: 11097.4583 miliseconds with peak memory at: 3164 663 808

*/