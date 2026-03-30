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

        internal string RunMergeSortSingleThreadTests()
        {
            var sb = new StringBuilder();
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test1).Span));
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test2).Span));
            sb.AppendLine(MergeSortSingleThread_TimerText(GetRandomTable(test3).Span));

            return sb.ToString();
        }


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
            Process currentProcess = Process.GetCurrentProcess();

            MergeSort(span);

            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
            currentProcess.Refresh();

            return "For Single Thread test size: " + span.Length +
                " sorting took: " + elapsed.TotalMilliseconds + 
                " miliseconds with peak memory at: " + currentProcess.PeakWorkingSet64;
        }

        private string MergeSortParallel_TimerText(Memory<int> memory)
        {
            long startTime = Stopwatch.GetTimestamp();
            Process currentProcess = Process.GetCurrentProcess();

            MergeSortParallel(memory, MaxDepth);

            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
            currentProcess.Refresh();

            return "For Multi Thread test size: " + memory.Length +
                " sorting took: " + elapsed.TotalMilliseconds +
                " miliseconds with peak memory at: " + currentProcess.PeakWorkingSet64;
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
