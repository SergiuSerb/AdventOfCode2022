using System;
using System.Collections.Generic;
using System.Linq;
using Day17.Models.Moves;
using Day17.Models.Rocks;
using Day17.Tools.Math;

namespace Day17.Models
{
    public class ListSimulator : ISimulator
    {
        private Dictionary<int, List<RockComponent>> _lastRun;

        public ulong Run(MoveContainer moveContainer, ulong rocksToSpawn)
        {
            Dictionary<int, List<RockComponent>> settledComponents = new Dictionary<int, List<RockComponent>>();
            
            List<RockComponent> componentsToCheck = new List<RockComponent>();
            RockFactory rockFactory = new RockFactory();

            for ( ulong rockIndex = 1; rockIndex <= rocksToSpawn; rockIndex++ )
            {
                Rock rock = rockFactory.Create(rockIndex);

                SetStartingPosition(rock, settledComponents);

                while ( true )
                {
                    componentsToCheck.Clear();

                    IMove move = moveContainer.GetNextMove();
                    move.Perform(rock);

                    for (int rowIndex = rock.CoordinateRow - 2; rowIndex < rock.CoordinateRow + 2; rowIndex++)
                    {
                        if (settledComponents.ContainsKey(rowIndex))
                        {
                            componentsToCheck.AddRange(settledComponents[rowIndex]);
                        }     
                    }

                    if (CollisionCalculator.DoesCollide(rock, componentsToCheck))
                    {
                        move.Rollback(rock);
                        
                        if (move is Down)
                        {
                            break;
                        }
                    }
                }
                
                foreach (RockComponent rockComponent in rock.GetRockComponentsInWorldCoordinates())
                {
                    if (settledComponents.ContainsKey(rockComponent.CoordinateRow))
                    {
                        settledComponents[rockComponent.CoordinateRow].Add(rockComponent);
                    }
                    else
                    {
                        settledComponents.Add(rockComponent.CoordinateRow, new List<RockComponent>() { rockComponent});
                    }
                }
            }

            _lastRun = settledComponents;
            return (ulong) settledComponents.Max(x => x.Key);
        }

        private static void SetStartingPosition(IPlaceable rock, IReadOnlyDictionary<int, List<RockComponent>> settledComponents)
        {
            if (!settledComponents.Any())
            {
                rock.CoordinateRow = 3;
            }
            else
            {
                rock.CoordinateRow = settledComponents[settledComponents.Max(x => x.Key)].Max(x => x.CoordinateRow) + 4;
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

        public Dictionary<int, List<RockComponent>> GetLastRun()
        {
            return _lastRun;
        }
    }
}