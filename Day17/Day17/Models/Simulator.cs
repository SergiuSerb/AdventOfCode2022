using System;
using System.Collections.Generic;
using System.Linq;
using Day17.Models.Moves;
using Day17.Models.Rocks;

namespace Day17.Models
{
    public class Simulator
    {
        private readonly Dictionary<long, List<RockComponent>> settledComponents;

        public Simulator()
        {
            settledComponents = new Dictionary<long, List<RockComponent>>();
        }

        public void Run(MoveContainer moveContainer)
        {
            List<RockComponent> componentsToCheck = new List<RockComponent>();

            Rock rock = null;
            for ( ulong rockIndex = 1; rockIndex <= 1000000000000; rockIndex++ )
            {
                if (rockIndex % 100000000 ==  0)
                {
                    Console.WriteLine(rockIndex);
                }
                rock = RockFactory.Create(rockIndex);

                SetStartingPosition(rock);

                while ( true )
                {
                    componentsToCheck.Clear();

                    IMove move = moveContainer.GetNextMove();
                    move.Perform(rock);

                    if (settledComponents.ContainsKey(rock.CoordinateRow / 10))
                    {
                        componentsToCheck.AddRange(settledComponents[rock.CoordinateRow / 10]);
                    }                   
                    
                    if (settledComponents.ContainsKey((rock.CoordinateRow / 10) - 1 ) )
                    {
                        componentsToCheck.AddRange(settledComponents[(rock.CoordinateRow / 10) - 1]);
                    }
                    
                    if (settledComponents.ContainsKey((rock.CoordinateRow / 10) + 1 ) )
                    {
                        componentsToCheck.AddRange(settledComponents[(rock.CoordinateRow / 10) + 1]);
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
                    if (settledComponents.ContainsKey(rockComponent.CoordinateRow / 10))
                    {
                        settledComponents[rockComponent.CoordinateRow / 10].Add(rockComponent);
                    }
                    else
                    {
                        settledComponents.Add(rockComponent.CoordinateRow / 10, new List<RockComponent>() { rockComponent});
                    }
                }
            }
            //Print(rock);
        }

        private void SetStartingPosition(Rock rock)
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

        public long GetTopHeight()
        {
            return settledComponents[settledComponents.Max(x => x.Key)].Max(x => x.CoordinateRow);
        }

        private void Print(Rock rock)
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

            foreach (KeyValuePair<long,List<RockComponent>> valuePair in settledComponents)
            {
                foreach (var component in valuePair.Value)
                {
                    representation[component.CoordinateRow + 1, component.CoordinateColumn] = '#';
                }
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