import random
import time
import psutil
import os

class SimpleThreadableMonteCarloPi:
    def __init__(self):
        self.test1 = 50_000_000
        self.test2 = 500_000_000
        self.test3 = 2_000_000_000
        # Random() w Pythonie jest bezpieczny wątkowo (AI... check)
        self.rand = random.Random()

    def run_single_thread(self) -> str: # NO GIL-Free variant
        results = []
        results.append(self._run_loop(self.test1))
        results.append(self._run_loop(self.test2))
        #results.append(self._run_loop(self.test3)) #too slow >> untestable...
        
        return "\n".join(results)

    def _run_loop(self, number_of_points: int) -> str:
        process = psutil.Process(os.getpid())
        start_time = time.perf_counter()
        points_inside_circle = 0
        
        # Lokalne referencje przyspieszają pętle w Pythonie (AI... check)
        random_func = self.rand.random
        
        for _ in range(number_of_points):
            x = random_func()
            y = random_func()
            if x * x + y * y <= 1.0:
                points_inside_circle += 1

        pi_estimate = 4.0 * points_inside_circle / number_of_points
        elapsed_ms = (time.perf_counter() - start_time) * 1000
        
        # Peak memory (RSS) in bytes
        peak_memory_bytes = process.memory_info().peak_wset if hasattr(process.memory_info(), 'peak_wset') else process.memory_info().rss

        return (f"For Single Thread test size: {number_of_points} "
                f"Pi estimate is: {pi_estimate} and took {elapsed_ms:.2f} "
                f"miliseconds to calculate with peak memory at: {peak_memory_bytes}")
