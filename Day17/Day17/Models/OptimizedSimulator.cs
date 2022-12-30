using System;
using System.Collections.Generic;
using System.Linq;
using Day17.Models.Moves;
using Day17.Models.Rocks;
using Day17.Tools.Math;

namespace Day17.Models
{
    public class OptimizedSimulator : ISimulator
    {
        private const int OptimizationLimit = 1000;

        public ulong Run(MoveContainer moveContainer, ulong rocksToSpawn)
        {
            Dictionary<int, List<RockComponent>> settledComponents = new Dictionary<int, List<RockComponent>>(OptimizationLimit + 3);
            ulong rowOffset = 0;
            
            List<RockComponent> componentsToCheck = new List<RockComponent>();
            RockFactory rockFactory = new RockFactory();

            for ( uint rockIndex = 1; rockIndex <= rocksToSpawn; rockIndex++ )
            {
                if (settledComponents.Count % ( OptimizationLimit + 2 ) == 0)
                {
                    rowOffset = OptimizeSettledComponents2(settledComponents, rowOffset);
                }
                
                Rock rock = rockFactory.Create(rockIndex);

                SetStartingPosition(rock, settledComponents);

                while ( true )
                {
                    IMove move = moveContainer.GetNextMove();
                    move.Perform(rock);

                    GetComponentsToCheck(rock, settledComponents, componentsToCheck);

                    if (CollisionCalculator.DoesCollide(rock, componentsToCheck))
                    {
                        move.Rollback(rock);
                        
                        if (move is Down)
                        {
                            break;
                        }
                    }
                }
                
                AddRockComponentsToSettledComponents(rock, settledComponents);
            }

            return (ulong) settledComponents.Max(x => x.Key) + rowOffset;
        }

        private static ulong OptimizeSettledComponents2(IDictionary<int, List<RockComponent>> settledComponents, ulong rowOffset)
        {
            List<List<RockComponent>> rockComponents = settledComponents.OrderBy(x => x.Key).TakeLast(3).Select(x => x.Value).ToList();
            settledComponents.Clear();

            rowOffset += OptimizationLimit;
            

            foreach (List<RockComponent> row in rockComponents)
            {
                row.ForEach(x => x.CoordinateRow -= OptimizationLimit);
                
                settledComponents.Add(row.First().CoordinateRow, row);
            }

            return rowOffset;
        }

        private static void AddRockComponentsToSettledComponents(Rock rock, IDictionary<int, List<RockComponent>> settledComponents)
        {
            foreach (RockComponent rockComponent in rock.GetRockComponentsInWorldCoordinates())
            {
                if (settledComponents.ContainsKey(rockComponent.CoordinateRow))
                {
                    settledComponents[rockComponent.CoordinateRow].Add(rockComponent);
                }
                else
                {
                    settledComponents.Add(rockComponent.CoordinateRow, new List<RockComponent> {rockComponent});
                }
            }
        }

        private static void GetComponentsToCheck(IPlaceable rock, IReadOnlyDictionary<int, List<RockComponent>> settledComponents, List<RockComponent> componentsToCheck)
        {
            componentsToCheck.Clear();
            for (int rowIndex = rock.CoordinateRow - 2; rowIndex < rock.CoordinateRow + 2; rowIndex++)
            {
                if (settledComponents.ContainsKey(rowIndex))
                {
                    componentsToCheck.AddRange(settledComponents[rowIndex]);
                }
            }
        }

        private static void SetStartingPosition(IPlaceable rock, IReadOnlyDictionary<int, List<RockComponent>> settledComponents)
        {
            if (!settledComponents.Any())
            {
                rock.CoordinateRow = 3;
            }
            else
            {
                rock.CoordinateRow = settledComponents.Max(x => x.Key) + 4;
            }
        }

        private void Print(Rock rock, Dictionary<int, List<RockComponent>> settledComponents)
        {
            char[,] representation = new char[rock.CoordinateRow + 20, 9];
            
            for (int i = representation.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    representation[i, j] = '.';
                }
                
                Console.WriteLine();
            }

            for (int i = 0; i < representation.GetLength(0); i++)
            {
                representation[i, 0] = '|';
                representation[i, representation.GetLength(1) - 1] = '|';
            }

            for (int i = 0; i < representation.GetLength(1); i++)
            {
                representation[0, i] = '_';
            }

            foreach (RockComponent component in settledComponents.SelectMany(valuePair => valuePair.Value))
            {
                representation[component.CoordinateRow + 1, component.CoordinateColumn] = '#';
            }

            if (rock != null)
            {
                foreach (RockComponent component in rock.GetRockComponentsInWorldCoordinates())
                {
                    representation[component.CoordinateRow + 1, component.CoordinateColumn] = '@';
                }
            }

            for (int i = representation.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < representation.GetLength(1); j++)
                {
                    Console.Write(representation[i, j]);
                }
                
                Console.WriteLine();
            }
        }
        
    }
}