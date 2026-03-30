from simple_threadable_monte_carlo import SimpleThreadableMonteCarloPi

print(f"Pi Monte Carlo Single Thread:")
mc = SimpleThreadableMonteCarloPi()
print(mc.run_single_thread())

"""
Pi Monte Carlo Single Thread:
For Single Thread test size:  50000000 Pi estimate is: 3.14130328  and took  6949.27 miliseconds to calculate with peak memory at: 18239488
For Single Thread test size: 500000000 Pi estimate is: 3.141591928 and took 69747.01 miliseconds to calculate with peak memory at: 18280448
500 mil ... did not launch it... 
"""