using System;
using System.Collections.Generic;
using System.Linq;
using Day17.Models.Moves;
using Day17.Models.Rocks;

namespace Day17.Models
{
    public class PredictiveSimulator : ISimulator
    {
        private readonly ListSimulatorWithLogging SimulatorForPatternFinding;
        private readonly DeltaRowPatternSearcherV2 DeltaRowSearcher;

        public PredictiveSimulator()
        {
            DeltaRowSearcher = new DeltaRowPatternSearcherV2();
            SimulatorForPatternFinding = new ListSimulatorWithLogging();
        }

        public ulong Run(MoveContainer moveContainer, ulong rockNumber)
        {
            ulong rocksToSpawn = (ulong) (moveContainer.MoveCount * RockFactory.RockTypesCount) / 3;
            SimulateRocks(moveContainer, rocksToSpawn);

            Pattern bestPattern = SearchForPattern();
            ulong startingIndex = rocksToSpawn - (ulong) (bestPattern.SequenceLength * bestPattern.OccurrenceFromEnd);
            Console.WriteLine($"Pattern of length {bestPattern.SequenceLength} starting after rockID {startingIndex} occuring {bestPattern.OccurenceCount} times, {bestPattern.OccurrenceFromEnd} from the end has been selected.");

            
            ulong heightToStartingRock = SimulateRocks(moveContainer, startingIndex);
            ulong heightWithPattern = SimulateRocks(moveContainer, startingIndex + (ulong) bestPattern.SequenceLength);
            ulong patternHeight = heightWithPattern - heightToStartingRock;

            ulong patternRepetitions = (rockNumber - startingIndex) / (ulong) bestPattern.SequenceLength;
            ulong remainingRocks = (rockNumber - startingIndex) % (ulong) bestPattern.SequenceLength;

            ulong heightWithPatternAndRemainingRocks = SimulateRocks(moveContainer,
                startingIndex + (ulong) bestPattern.SequenceLength + remainingRocks);
            ulong heightOfRemainingRocks = heightWithPatternAndRemainingRocks - heightWithPattern;
            
            ulong predictedHeight = patternRepetitions * patternHeight;

            return predictedHeight + heightToStartingRock + heightOfRemainingRocks;
        }

        private Pattern SearchForPattern()
        {
            Console.WriteLine($"Searching for a pattern...");
            List<Pattern> patterns = DeltaRowSearcher.Search(SimulatorForPatternFinding.LogEntries);

            return patterns.OrderByDescending(x => x.OccurrenceFromEnd).First();
        }

        private ulong SimulateRocks(MoveContainer moveContainer, ulong rocksToSpawn)
        {
            Console.WriteLine($"Simulating {rocksToSpawn} rocks...");
            moveContainer.Reset();
            return SimulatorForPatternFinding.Run(moveContainer, rocksToSpawn);
        }
    }
}