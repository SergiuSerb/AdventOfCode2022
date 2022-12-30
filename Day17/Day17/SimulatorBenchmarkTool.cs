using System;
using System.Diagnostics;
using Day17.Models;
using Day17.Models.Moves;

namespace Day17
{
    public class SimulatorBenchmarkTool
    {
        
        public void StartBenchmark(MoveContainer moveContainer)
        {
            Stopwatch stopwatch = new Stopwatch();
            OptimizedSimulator optimizedSimulator = new OptimizedSimulator();
            Simulator simulator = new Simulator();
            ListSimulator listSimulator = new ListSimulator();
            PredictiveSimulator predictiveSimulator = new PredictiveSimulator();
            
            for (ulong numberOfRocks = 1000; numberOfRocks <= 20000; numberOfRocks+= 5000)
            {
                moveContainer.Reset();
                stopwatch.Reset();
                stopwatch.Start();
                ulong resultSimulator = simulator.Run(moveContainer, numberOfRocks);
                stopwatch.Stop();
                long simulatorElapsed = stopwatch.ElapsedMilliseconds;
                
                moveContainer.Reset();
                stopwatch.Reset();
                stopwatch.Start();
                ulong resultListSimulator = listSimulator.Run(moveContainer, numberOfRocks);
                stopwatch.Stop();
                long simulatorListElapsed = stopwatch.ElapsedMilliseconds;

                moveContainer.Reset();
                stopwatch.Reset();
                stopwatch.Start();
                ulong resultPredictiveSimulator = predictiveSimulator.Run(moveContainer, numberOfRocks);
                stopwatch.Stop();
                long simulatorPredictiveElapsed = stopwatch.ElapsedMilliseconds;

                moveContainer.Reset();
                stopwatch.Reset();
                stopwatch.Start();
                ulong resultOptimizedSimulator = optimizedSimulator.Run(moveContainer, numberOfRocks);
                stopwatch.Stop();
                long simulatorOptimizedElapsed = stopwatch.ElapsedMilliseconds;

                Console.WriteLine(
                    $"For {numberOfRocks} rocks: \n - simulator returned {resultSimulator} in {simulatorElapsed}ms \n - listSimulator returned {resultListSimulator} in {simulatorListElapsed}ms \n - predictive simulator returned {resultPredictiveSimulator} in {simulatorPredictiveElapsed}ms \n - optimized simulator returned {resultOptimizedSimulator} in {simulatorOptimizedElapsed}ms");
            }
        }
    }
}