using System;
using System.Collections.Generic;
using System.Linq;
using Day17.Models.Moves;
using Day17.Models.Rocks;
using Day17.Tools.Math;

namespace Day17.Models
{
    public class Simulator : ISimulator
    {
        public ulong Run(MoveContainer moveContainer, ulong rocksToSpawn)
        {
            Dictionary<int, List<RockComponent>> settledComponents = new Dictionary<int, List<RockComponent>>();
            List<RockComponent> componentsToCheck = new List<RockComponent>();
            RockFactory rockFactory = new RockFactory();

            for ( uint rockIndex = 1; rockIndex <= rocksToSpawn; rockIndex++ )
            {
                Rock rock = rockFactory.Create(rockIndex);

                SetStartingPosition(rock, settledComponents);

                while ( true )
                {
                    componentsToCheck.Clear();

                    IMove move = moveContainer.GetNextMove();
                    move.Perform(rock);

                    int rockDictionaryKey = rock.CoordinateRow / 10;

                    if (settledComponents.ContainsKey(rockDictionaryKey))
                    {
                        componentsToCheck.AddRange(settledComponents[rockDictionaryKey]);
                    }                   
                    
                    if (settledComponents.ContainsKey(rockDictionaryKey - 1 ) )
                    {
                        componentsToCheck.AddRange(settledComponents[rockDictionaryKey - 1]);
                    }
                    
                    if (settledComponents.ContainsKey(rockDictionaryKey + 1 ) )
                    {
                        componentsToCheck.AddRange(settledComponents[rockDictionaryKey + 1]);
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
                    int rockDictionaryKey = rockComponent.CoordinateRow / 10;
                    if (settledComponents.ContainsKey(rockDictionaryKey))
                    {
                        settledComponents[rockDictionaryKey].Add(rockComponent);
                    }
                    else
                    {
                        settledComponents.Add(rockDictionaryKey, new List<RockComponent>() { rockComponent});
                    }
                }
            }
            
            //return rock.GetRockComponentsInWorldCoordinates().Max(x => x.CoordinateRow);
            return (ulong) settledComponents.SelectMany(x => x.Value).Max(x => x.CoordinateRow);
        }

        private static void SetStartingPosition(IPlaceable rock, IReadOnlyDictionary<int, List<RockComponent>> settledComponents)
        {
            if (!settledComponents.Any())
            {
                rock.CoordinateRow = 3; // + rock.RowSpan;
            }
            else
            {
                rock.CoordinateRow = settledComponents[settledComponents.Max(x => x.Key)].Max(x => x.CoordinateRow) + 4;// + rock.RowSpan;
            }
        }

        // public int GetTopHeight()
        // {
        //     return settledComponents[settledComponents.Max(x => x.Key)].Max(x => x.CoordinateRow);
        // }

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