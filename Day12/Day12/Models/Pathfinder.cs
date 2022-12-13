using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12.Models
{
    internal class Pathfinder
    {
        private readonly IList<IList<Tile>> _tiles;

        private readonly Tile _source;

        private readonly Tile _target;

        public Pathfinder( IList<IList<Tile>> tiles )
        {
            _tiles = tiles;
            _source = _tiles.First( x => x.Any( x => x.TileType == TileType.Source ) )
                            .First( x => x.TileType == TileType.Source );
            _target = _tiles.First( x => x.Any( x => x.TileType == TileType.Target ) )
                            .First( x => x.TileType == TileType.Target );
        }

        public void FindPath()
        {
            Tile currentTile = _source;
            List<Tile> visitedTiles = new List<Tile>();
            List<Tile> tilesToVisit = new List<Tile>();
            
            while ( currentTile != _target && currentTile != null )
            {
                visitedTiles.Add(currentTile);
                var possibleTiles = new List<Tile>();
                
                if (currentTile.CoordinatesRow + 1 < MapSettings.height)
                {
                    possibleTiles.Add(_tiles[currentTile.CoordinatesRow + 1][currentTile.CoordinatesColumn]);
                }
                
                if (currentTile.CoordinatesRow - 1 >= 0)
                {
                    possibleTiles.Add(_tiles[currentTile.CoordinatesRow - 1][currentTile.CoordinatesColumn]);
                }
                
                if (currentTile.CoordinatesColumn + 1 < MapSettings.width)
                {
                    possibleTiles.Add(_tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn + 1]);
                }                
                
                if (currentTile.CoordinatesColumn - 1 >= 0)
                {
                    possibleTiles.Add(_tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn - 1]);
                }
                
                var filteredTiles =
                    possibleTiles.Where(x => x.Height == currentTile.Height + 1 || x.Height == currentTile.Height)
                        .Where(x => !visitedTiles.Contains(x))
                        .Where(x => (tilesToVisit.Contains(x) && x.DistanceToTarget > GetDistanceBetweenTiles(x, _target)) || !tilesToVisit.Contains(x));
                    
                foreach (Tile filteredTile in filteredTiles)
                {
                    filteredTile.Neighbour = currentTile;
                    filteredTile.DistanceToTarget = GetDistanceBetweenTiles(filteredTile, _target);
                    filteredTile.CostFromSource = currentTile.CostFromSource + GetDistanceBetweenTiles(currentTile, filteredTile);
                }
                
                tilesToVisit.AddRange(filteredTiles.Where(x => !tilesToVisit.Contains(x)));
                currentTile = tilesToVisit.OrderBy(x => x.DistanceToTarget).FirstOrDefault();
                tilesToVisit.Remove(currentTile);
            }
        }

        private int GetDistanceBetweenTiles(Tile leftOperand, Tile rightOperand)
        {
            return Math.Abs(leftOperand.CoordinatesRow - rightOperand.CoordinatesRow) +
                   Math.Abs(leftOperand.CoordinatesColumn - rightOperand.CoordinatesColumn);
        }
    }
}