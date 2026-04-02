from simple_threadable_monte_carlo import SimpleThreadableMonteCarloPi
import sys
import sysconfig

print("test")

#To check whether the Python interpreter you're using is a free-threaded build, irrespective of whether the GIL was re-enabled at runtime or not, you can use this within your code:
#why false since I installed with experimental extension opition checked...
is_freethreaded = bool(sysconfig.get_config_var("Py_GIL_DISABLED"))
print(f"is_freethreaded? {is_freethreaded}")

if hasattr(sys, "_is_gil_enabled"): # from 3.13...
    print(f"Is GIL enabled? {sys._is_gil_enabled()} (it should be 'false' in order for multi threading tests to work properly)")
    
else:
    print("This version of Python does not support the optional GIL.")

    
mc = SimpleThreadableMonteCarloPi()

print(f"Pi Monte Carlo Single Thread:")
print(mc.run_single_thread())

print(f"Pi Monte Carlo Multi Thread")
print(mc.run_multi_thread())




"""
(how come 18GB when PC is only 16GB?...)
Pi Monte Carlo Single Thread:
For Single Thread test size:  50000000 Pi estimate is: 3.14130328  and took  6949.27 miliseconds to calculate with peak memory at: 18 239488
For Single Thread test size: 500000000 Pi estimate is: 3.141591928 and took 69747.01 miliseconds to calculate with peak memory at: 18 280448
500 mil ... did not launch it... 

(failure !!!! with GIL enabled...) (how come 18GB when PC is only 16GB?...)
Pi Monte Carlo Multi Thread GIL-free:
For Multi-Thread (8 threads) test size:  50000000 Pi estimate is:  3.14168296 and took  13318.13 ms with peak memory at: 22 204416 bytes
For Multi-Thread (8 threads) test size: 500000000 Pi estimate is: 3.141644344 and took 130250.31 ms with peak memory at: 22 261760 bytes

(GIL disabled run: unsatisfying results...)
is_freethreaded? True
Is GIL enabled? False (it should be 'false' in order for multi threading tests to work properly)
Pi Monte Carlo Single Thread:
For Single Thread test size: 50000000 Pi estimate is: 3.14123232 and took 8108.70 miliseconds to calculate with peak memory at: 25 059 328
Pi Monte Carlo Multi Thread
For Multi-Thread (8 threads) test size: 50000000 Pi estimate is: 3.1415072 and took 76733.83 ms with peak memory at: 27 086 848 bytes

No point in testing merge sort...
"""