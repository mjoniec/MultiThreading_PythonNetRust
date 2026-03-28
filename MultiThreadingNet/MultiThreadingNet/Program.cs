namespace MultiThreadingNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pi Monte Carlo Single Thread:");
            Console.WriteLine(new SimpleThreadableMonteCarloPi().RunSingleThread());
            Console.WriteLine("Pi Monte Carlo Multi Threaded:");
            Console.WriteLine(new SimpleThreadableMonteCarloPi().RunmultiThreaded());
        }
    }
}
