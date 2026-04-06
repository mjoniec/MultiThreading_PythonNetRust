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

release:

Pi Monte Carlo Single Thread:
For Single Thread test size:   50 000 000 Pi estimate is: 3.14134576  and took   460.851  miliseconds to calculate with peak memory at: 22 466 560
For Single Thread test size:  500 000 000 Pi estimate is: 3.141679768 and took  4469.8717 miliseconds to calculate with peak memory at: 24 141 824
For Single Thread test size: 2000 000 000 Pi estimate is: 3.14163663  and took 17788.4582 miliseconds to calculate with peak memory at: 24 166 400

Pi Monte Carlo Multi Threaded:
For Multi Threaded test size:   50 000 000 Pi estimate is: 3.14177368  and took  145.1713 miliseconds to calculate with peak memory at: 26 697 728
For Multi Threaded test size:  500 000 000 Pi estimate is: 3.141500296 and took 1192.498  miliseconds to calculate with peak memory at: 27 328 512
For Multi Threaded test size: 2000 000 000 Pi estimate is: 3.141577274 and took 4804.563  miliseconds to calculate with peak memory at: 28 356 608

Merge Sort Single Thread:
For Single Thread test size:   1 000 000 sorting took:   197.9436 miliseconds with peak memory at:   49 446912
For Single Thread test size:  10 000 000 sorting took:  1177.8297 miliseconds with peak memory at:  286 613504
For Single Thread test size: 100 000 000 sorting took: 12873.1572 miliseconds with peak memory at: 1798 041600

Merge Sort Multi Threaded:
For Multi Thread test size:   1 000 000 sorting took:   35.2071 miliseconds with peak memory at: 1 798 041600
For Multi Thread test size:  10 000 000 sorting took:  369.5025 miliseconds with peak memory at: 1 798 041600
For Multi Thread test size: 100 000 000 sorting took: 4094.7963 miliseconds with peak memory at: 3 436 490752




debug:
Pi Monte Carlo Single Thread:
For Single Thread test size:   50 000 000 Pi estimate is:  3.14153608 and took  1008.8628 miliseconds to calculate with peak memory at: 35 618 816
For Single Thread test size:  500 000 000 Pi estimate is:  3.14152424 and took  7520.3479 miliseconds to calculate with peak memory at: 37 470 208
For Single Thread test size: 2000 000 000 Pi estimate is: 3.141553622 and took 29547.1995 miliseconds to calculate with peak memory at: 37 707 776

Pi Monte Carlo Multi Threaded:
For Multi Threaded test size:   50 000 000 Pi estimate is: 3.14168872  and took  525.9743 miliseconds to calculate with peak memory at: 35 360 768
For Multi Threaded test size:  500 000 000 Pi estimate is: 3.141532776 and took 2543.7616 miliseconds to calculate with peak memory at: 36 601 856
For Multi Threaded test size: 2000 000 000 Pi estimate is: 3.141625438 and took 9615.4509 miliseconds to calculate with peak memory at: 38 166 528

Merge Sort Single Thread::
For Single Thread test size:   1 000 000 sorting took:   452.6238 miliseconds with peak memory at:   54 886 400
For Single Thread test size:  10 000 000 sorting took:  3604.2488 miliseconds with peak memory at:  294 260 736
For Single Thread test size: 100 000 000 sorting took: 39651.9805 miliseconds with peak memory at: 1740 173 312

Merge Sort Multi Threaded:
For Multi Thread test size:   1 000 000 sorting took:   166.0000 miliseconds with peak memory at: 1 740 173 312
For Multi Thread test size:  10 000 000 sorting took:   978.5661 miliseconds with peak memory at: 1 740 173 312
For Multi Thread test size: 100 000 000 sorting took: 11097.4583 miliseconds with peak memory at: 3 164 663 808

*/