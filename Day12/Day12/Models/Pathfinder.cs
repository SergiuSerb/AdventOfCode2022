using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12.Models
{
    internal class Pathfinder
    {
        public readonly IList<IList<Tile>> Tiles;

        private Tile _source;

        private Tile _target;
        
        private IList<Tile> _solution;

        public Pathfinder( IList<IList<Tile>> tiles )
        {
            Tiles = tiles;
        }

        public IList<Tile> FindPath(Tile source, Tile target)
        {
            _source = source;
            _target = target;
            
            Tile currentTile = _source;
            List<Tile> visitedTiles = new List<Tile>();
            List<Tile> tilesToVisit = new List<Tile>();
            
            while ( currentTile != _target && currentTile != null )
            {
                visitedTiles.Add(currentTile);
                var possibleTiles = new List<Tile>();
                
                if (currentTile.CoordinatesRow + 1 < MapSettings.height)
                {
                    possibleTiles.Add(Tiles[currentTile.CoordinatesRow + 1][currentTile.CoordinatesColumn]);
                }
                
                if (currentTile.CoordinatesRow - 1 >= 0)
                {
                    possibleTiles.Add(Tiles[currentTile.CoordinatesRow - 1][currentTile.CoordinatesColumn]);
                }
                
                if (currentTile.CoordinatesColumn + 1 < MapSettings.width)
                {
                    possibleTiles.Add(Tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn + 1]);
                }                
                
                if (currentTile.CoordinatesColumn - 1 >= 0)
                {
                    possibleTiles.Add(Tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn - 1]);
                }

                var filteredTiles =
                    possibleTiles.Where(x => x.Height <= currentTile.Height + 1)
                        .Where(x => !visitedTiles.Contains(x))
                        .Where(x => (tilesToVisit.Contains(x) && x.Fitness > currentTile.CostFromSource + GetDistanceBetweenTiles(x, _target)) || !tilesToVisit.Contains(x));
                    
                foreach (Tile filteredTile in filteredTiles)
                {
                    filteredTile.Neighbour = currentTile;
                    filteredTile.DistanceToTarget = GetDistanceBetweenTiles(filteredTile, _target);
                    filteredTile.CostFromSource = currentTile.CostFromSource + GetDistanceBetweenTiles(currentTile, filteredTile);
                }
                
                tilesToVisit.AddRange(filteredTiles.Where(x => !tilesToVisit.Contains(x)));
                currentTile = tilesToVisit.OrderBy(x => x.Fitness).FirstOrDefault();
                tilesToVisit.Remove(currentTile);
            }
            
            IList<Tile> path = new List<Tile>();

            while (currentTile != _source && currentTile != null)
            {
                path.Add(currentTile);
                currentTile = currentTile.Neighbour;
            }

            _solution = path;
            
            return path;
        }

        public void PrintPath()
        {
            for (int i = 0; i < MapSettings.height; i++)
            {
                for (int j = 0; j < MapSettings.width; j++)
                {
                    Console.BackgroundColor = Tiles[i][j].TileType switch
                    {
                        TileType.Source => ConsoleColor.Red,
                        TileType.Intermediary => ConsoleColor.Black,
                        TileType.Target => ConsoleColor.Green,
                        _ => Console.BackgroundColor
                    };

                    Console.ForegroundColor = _solution.Contains(Tiles[i][j]) ? ConsoleColor.Green : ConsoleColor.White;

                    if (Tiles[i][j].Neighbour == null)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        if (Tiles[i][j].CoordinatesRow > Tiles[i][j].Neighbour.CoordinatesRow )
                        {
                            Console.Write("▼");
                            continue;
                        }
                        
                        if (Tiles[i][j].CoordinatesRow < Tiles[i][j].Neighbour.CoordinatesRow )
                        {
                            Console.Write("▲");
                            continue;
                        }
                        
                        if (Tiles[i][j].CoordinatesColumn > Tiles[i][j].Neighbour.CoordinatesColumn )
                        {
                            Console.Write("►");
                            continue;
                        }
                        
                        if (Tiles[i][j].CoordinatesColumn < Tiles[i][j].Neighbour.CoordinatesColumn )
                        {
                            Console.Write("◄");
                            continue;
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        private int GetDistanceBetweenTiles(Tile leftOperand, Tile rightOperand)
        {
            return Math.Abs(leftOperand.CoordinatesRow - rightOperand.CoordinatesRow) +
                   Math.Abs(leftOperand.CoordinatesColumn - rightOperand.CoordinatesColumn);
        }
    }
}