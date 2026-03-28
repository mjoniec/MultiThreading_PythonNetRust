using System.Diagnostics;
using System.Text;

namespace MultiThreadingNet
{
    internal class HardThreadableMergeSort
    {
        int test1 =   1_000_000;
        int test2 =  10_000_000;
        int test3 = 100_000_000;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Merge Sort Single Thread::
        /// For Single Thread test size: 1000000 sorting took: 455.5557 miliseconds
        /// For Single Thread test size: 10000000 sorting took: 3816.4044 miliseconds
        /// For Single Thread test size: 100000000 sorting took: 40878.3455 miliseconds
        /// </returns>
        internal string RunMergeSortSingleThread()
        {
            var sb = new StringBuilder();
            sb.AppendLine(MergeSortSingleThread(test1));
            sb.AppendLine(MergeSortSingleThread(test2));
            sb.AppendLine(MergeSortSingleThread(test3));

            return sb.ToString();
        }

        private string MergeSortSingleThread(int n)
        {
            long startTime = Stopwatch.GetTimestamp();

            MergeSort(GetRandomTable(n));

            TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);

            return "For Single Thread test size: " + n + 
                " sorting took: " + elapsed.TotalMilliseconds + " miliseconds";
        }

        static Span<int> GetRandomTable(int n)
        {
            var rand = new Random();
            int[] tab = new int[n];

            for (int i = 0; i < n; i++) tab[i] = rand.Next(1, int.MaxValue);

            Span<int> span = tab;

            return span;
        }

        private void MergeSort(Span<int> array)
        {
            if (array.Length <= 1) return;

            int mid = array.Length / 2;

            MergeSort(array.Slice(0, mid));
            MergeSort(array.Slice(mid));
            Merge(array, mid);
        }

        private void Merge(Span<int> array, int mid)
        {
            int[] temp = new int[array.Length];
            int i = 0, j = mid, k = 0;

            while (i < mid && j < array.Length) //copy smaller to temp
            {
                if (array[i] <= array[j]) temp[k++] = array[i++];
                else temp[k++] = array[j++];
            }

            while (i < mid) temp[k++] = array[i++]; // copy remaining (if any) 
            while (j < array.Length) temp[k++] = array[j++];

            temp.AsSpan().CopyTo(array); //move temp back to sorted table
        }
    }
}
