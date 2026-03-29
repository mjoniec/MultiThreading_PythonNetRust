using System.Diagnostics;
using System.Text;

namespace MultiThreadingNet
{
    internal class HardThreadableMergeSort
    {
        int test1 =   1_000_000;
        int test2 =  10_000_000;
        int test3 = 100_000_000;
        int Threshold = 2048; //under that we use Single Thread Merge Sort
        int MaxDepth = (int)Math.Log2(Environment.ProcessorCount); // max depth based on core count

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Merge Sort Single Thread::
        /// For Single Thread test size: 1000000 sorting took: 455.5557 miliseconds
        /// For Single Thread test size: 10000000 sorting took: 3816.4044 miliseconds
        /// For Single Thread test size: 100000000 sorting took: 40878.3455 miliseconds
        /// </returns>
        internal string RunMergeSortSingleThreadTests()
        {
            var sb = new StringBuilder();
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test1).Span));
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test2).Span));
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test3).Span));

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Merge Sort Multi Threaded:
        /// For Multi Thread test size: 1000000 sorting took: 279.7863 miliseconds
        /// For Multi Thread test size: 10000000 sorting took: 970.846 miliseconds
        /// For Multi Thread test size: 100000000 sorting took: 10637.8786 miliseconds
        /// </returns>
        internal string RunMergeSortMultiThreadedTests()
        {
            var sb = new StringBuilder();
            sb.AppendLine(MergeSortParallel_TimerText(GetRandomTable(test1)));
            sb.AppendLine(MergeSortParallel_TimerText(GetRandomTable(test2)));
            sb.AppendLine(MergeSortParallel_TimerText(GetRandomTable(test3)));

            return sb.ToString();
        }

        private string MergeSortSingleThread_TimerText(Span<int> span)
        {
            long startTime = Stopwatch.GetTimestamp();

            MergeSort(span);

            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);

            return "For Single Thread test size: " + span.Length +
                " sorting took: " + elapsed.TotalMilliseconds + " miliseconds";
        }

        private string MergeSortParallel_TimerText(Memory<int> memory)
        {
            long startTime = Stopwatch.GetTimestamp();

            MergeSortParallel(memory, MaxDepth);

            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);

            return "For Multi Thread test size: " + memory.Length +
                " sorting took: " + elapsed.TotalMilliseconds + " miliseconds";
        }

        private void MergeSortParallel(Memory<int> memory, int depth)
        {
            if (memory.Length < Threshold || depth <= 0) //sequential
            {
                MergeSort(memory.Span);
                return;
            }

            int mid = memory.Length / 2;
            var left = memory.Slice(0, mid);
            var right = memory.Slice(mid);

            Parallel.Invoke(
                () => MergeSortParallel(left, depth - 1),
                () => MergeSortParallel(right, depth - 1)
            );

            Merge(memory.Span, mid); //one thread merge per pair
        }

        private void MergeSort(Span<int> span)
        {
            if (span.Length <= 1) return;

            int mid = span.Length / 2;

            MergeSort(span.Slice(0, mid));
            MergeSort(span.Slice(mid));
            Merge(span, mid);
        }

        private void Merge(Span<int> span, int mid)
        {
            int[] temp = new int[span.Length];
            int i = 0, j = mid, k = 0;

            while (i < mid && j < span.Length) //copy smaller to temp
            {
                if (span[i] <= span[j]) temp[k++] = span[i++];
                else temp[k++] = span[j++];
            }

            while (i < mid) temp[k++] = span[i++]; // copy remaining (if any) 
            while (j < span.Length) temp[k++] = span[j++];

            temp.AsSpan().CopyTo(span); //move temp back to sorted table
        }

        private static Memory<int> GetRandomTable(int n)
        {
            var rand = new Random();
            int[] tab = new int[n];

            for (int i = 0; i < n; i++) tab[i] = rand.Next(1, int.MaxValue);

            return tab.AsMemory();
        }
    }
}
