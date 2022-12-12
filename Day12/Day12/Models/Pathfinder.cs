using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12.Models
{
    internal class Pathfinder
    {
        private IList<IList<Tile>> _tiles;

        private Tile _source;

        private Tile _target;

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

            while ( currentTile != _target )
            {
                var possibleTiles = new List<Tile>()
                {
                    _tiles[currentTile.CoordinatesRow + 1][currentTile.CoordinatesColumn],
                    _tiles[currentTile.CoordinatesRow - 1][currentTile.CoordinatesColumn],
                    _tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn + 1],
                    _tiles[currentTile.CoordinatesRow][currentTile.CoordinatesColumn - 1]
                };

                var filteredTiles =
                    possibleTiles.Where( x => x.Height + 1 == currentTile.Height || x.Height == currentTile.Height );
            }
        }
    }
}