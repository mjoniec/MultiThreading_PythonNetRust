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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Pi Monte Carlo Single Thread:
        /// For Single Thread test size: 50000000 Pi estimate is: 3.14167312 and took 1076.2867 miliseconds to calculate
        /// For Single Thread test size: 500000000 Pi estimate is: 3.1415828 and took 7496.23 miliseconds to calculate
        /// For Single Thread test size: 2000000000 Pi estimate is: 3.141538216 and took 29947.0328 miliseconds to calculate
        /// </returns>
        internal string RunSingleThread()
        {
            var sb = new StringBuilder();
            sb.AppendLine(RunLoop(test1));
            sb.AppendLine(RunLoop(test2));
            sb.AppendLine(RunLoop(test3));
            
            return sb.ToString();

            string RunLoop(long numberOfPoints)
            {
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

                var s = "For Single Thread test size: " + numberOfPoints + 
                    " Pi estimate is: " + piEstimate + " and took " + elapsed.TotalMilliseconds + " miliseconds to calculate";
                
                return s;
            }
        }
    }
}
