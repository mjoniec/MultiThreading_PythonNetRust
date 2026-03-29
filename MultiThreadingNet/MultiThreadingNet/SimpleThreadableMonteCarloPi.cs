using System.Diagnostics;
using System.Text;

namespace MultiThreadingNet
{
    internal class SimpleThreadableMonteCarloPi
    {
        long test1 =    50_000_000;
        long test2 =   500_000_000;
        long test3 = 2_000_000_000;
        Random rand = new Random();


        /* Pi Monte Carlo Single Thread:
For Single Thread test size:   50000000 Pi estimate is:  3.14153608 and took  1008.8628 miliseconds to calculate with peak memory at: 35618816
For Single Thread test size:  500000000 Pi estimate is:  3.14152424 and took  7520.3479 miliseconds to calculate with peak memory at: 37470208
For Single Thread test size: 2000000000 Pi estimate is: 3.141553622 and took 29547.1995 miliseconds to calculate with peak memory at: 37707776
        */
        internal string RunSingleThread()
        {
            var sb = new StringBuilder();
            sb.AppendLine(RunLoop(test1));
            sb.AppendLine(RunLoop(test2));
            sb.AppendLine(RunLoop(test3));
            
            return sb.ToString();

            string RunLoop(long numberOfPoints)
            {
                Process currentProcess = Process.GetCurrentProcess();
                long startTime = Stopwatch.GetTimestamp();
                long pointsInsideCircle = 0;
                
                for (long i = 0; i < numberOfPoints; i++)
                {
                    double x = rand.NextDouble();
                    double y = rand.NextDouble();

                    if (x * x + y * y <= 1.0) pointsInsideCircle++;
                }

                double piEstimate = 4.0 * pointsInsideCircle / numberOfPoints;
                TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
                currentProcess.Refresh();
                long peakMemoryBytes = currentProcess.PeakWorkingSet64;

                var s = "For Single Thread test size: " + numberOfPoints + 
                    " Pi estimate is: " + piEstimate + " and took " + elapsed.TotalMilliseconds 
                    + " miliseconds to calculate with peak memory at: " + peakMemoryBytes;
                
                return s;
            }
        }

        /* Pi Monte Carlo Multi Threaded:
For Multi Threaded test size:   50000000 Pi estimate is: 3.14168872  and took  525.9743 miliseconds to calculate with peak memory at: 35360768
For Multi Threaded test size:  500000000 Pi estimate is: 3.141532776 and took 2543.7616 miliseconds to calculate with peak memory at: 36601856
For Multi Threaded test size: 2000000000 Pi estimate is: 3.141625438 and took 9615.4509 miliseconds to calculate with peak memory at: 38166528
        */
        internal string RunMultiThreaded()
        {
            var sb = new StringBuilder();
            sb.AppendLine(RunLoop(test1));
            sb.AppendLine(RunLoop(test2));
            sb.AppendLine(RunLoop(test3));

            return sb.ToString();

            string RunLoop(long numberOfPoints)
            {
                Process currentProcess = Process.GetCurrentProcess();
                long startTime = Stopwatch.GetTimestamp();
                long pointsInsideCircle = 0;

                Parallel.For(0, numberOfPoints,
                    () => 0L, // local counter per thread
                    (i, state, localCount) =>
                    {
                        double x = Random.Shared.NextDouble();
                        double y = Random.Shared.NextDouble();
                        if (x * x + y * y <= 1.0) localCount++;
                        return localCount;
                    },
                    localCount => Interlocked.Add(ref pointsInsideCircle, localCount) //sum up
                );

                double piEstimate = 4.0 * pointsInsideCircle / numberOfPoints;
                TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
                currentProcess.Refresh();
                long peakMemoryBytes = currentProcess.PeakWorkingSet64;

                var s = "For Multi Threaded test size: " + numberOfPoints +
                    " Pi estimate is: " + piEstimate + " and took " + elapsed.TotalMilliseconds 
                    + " miliseconds to calculate with peak memory at: " + peakMemoryBytes;

                return s;
            }
        }
    }
}
