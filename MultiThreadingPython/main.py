from simple_threadable_monte_carlo import SimpleThreadableMonteCarloPi
import sys

if hasattr(sys, "_is_gil_enabled"): # from 3.13...
    print(f"Is GIL enabled? (it should not be for tests to properly work)  {sys._is_gil_enabled()}")
else:
    print("This version of Python does not support the optional GIL.")

mc = SimpleThreadableMonteCarloPi()

#print(f"Pi Monte Carlo Single Thread:")
#print(mc.run_single_thread())

print(f"Pi Monte Carlo Multi Thread")
print(mc.run_multi_thread())




"""
(how come 18GB when PC is only 16GB?...)
Pi Monte Carlo Single Thread:
For Single Thread test size:  50000000 Pi estimate is: 3.14130328  and took  6949.27 miliseconds to calculate with peak memory at: 18 239488
For Single Thread test size: 500000000 Pi estimate is: 3.141591928 and took 69747.01 miliseconds to calculate with peak memory at: 18 280448
500 mil ... did not launch it... 

(failure !!!! double the time to a single core!!! not all ai generated stuff is good...)
(how come 18GB when PC is only 16GB?...)
Pi Monte Carlo Multi Thread GIL-free:
For Multi-Thread (8 threads) test size:  50000000 Pi estimate is:  3.14168296 and took  13318.13 ms with peak memory at: 22 204416 bytes
For Multi-Thread (8 threads) test size: 500000000 Pi estimate is: 3.141644344 and took 130250.31 ms with peak memory at: 22 261760 bytes



"""