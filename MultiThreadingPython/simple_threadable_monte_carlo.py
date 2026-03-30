import random
import time
import psutil
import os
from concurrent.futures import ThreadPoolExecutor

class SimpleThreadableMonteCarloPi:
    def __init__(self):
        self.test1 = 50_000_000
        self.test2 = 500_000_000
        self.test3 = 2_000_000_000
        # Random() w Pythonie jest bezpieczny wątkowo (AI... check)
        self.rand = random.Random()

    def run_single_thread(self) -> str: # NO GIL-Free variant
        results = []
        results.append(self.single_thread(self.test1))
        results.append(self.single_thread(self.test2))
        #results.append(self.single_thread(self.test3)) #too slow >> untestable...
        return "\n".join(results)

    def run_multi_thread(self) -> str: # GIL-Free variant
        results = []
        results.append(self.multi_thread(self.test1))
        results.append(self.multi_thread(self.test2))
        #results.append(self.multi_thread(self.test3))
        return "\n".join(results)

    def single_thread(self, number_of_points: int) -> str:
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

    def multi_thread(self, number_of_points: int) -> str: #SI follow up to previous SI re-generated code to include GIL-free
        process = psutil.Process(os.getpid())
        start_time = time.perf_counter()
        
        # Pobieramy liczbę dostępnych rdzeni
        num_workers = os.cpu_count() or 1 #returns 8 on 4 cores with double thread each...
        #num_workers = 4 #tmp solution
        points_per_worker = number_of_points // num_workers

        # Wykonanie równoległe (GIL-free w Python 3.13/3.14)
        with ThreadPoolExecutor(max_workers=num_workers) as executor:
            # Każdy wątek wykonuje część obliczeń
            results = list(executor.map(self._worker_task, [points_per_worker] * num_workers))
            
        points_inside_circle = sum(results)
        pi_estimate = 4.0 * points_inside_circle / number_of_points
        elapsed_ms = (time.perf_counter() - start_time) * 1000
        
        # Odczyt szczytowego zużycia pamięci (RSS)
        mem_info = process.memory_info()
        peak_memory = getattr(mem_info, 'peak_wset', mem_info.rss)

        return (f"For Multi-Thread ({num_workers} threads) test size: {number_of_points} "
                f"Pi estimate is: {pi_estimate} and took {elapsed_ms:.2f} ms "
                f"with peak memory at: {peak_memory} bytes")

    def _worker_task(self, n: int) -> int:
        """Funkcja wykonywana przez pojedynczy wątek."""
        inside = 0
        # Lokalne przypisanie funkcji drastycznie przyspiesza pętlę w Pythonie
        _random = random.random
        for _ in range(n):
            if _random()**2 + _random()**2 <= 1.0:
                inside += 1
        return inside
