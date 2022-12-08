using System;

namespace Day8
{
    public class Tree
    {
        public int Id { get; }
        
        public int Height { get; }

        public int ScenicScore => GetTreeScenicScore();

        public bool IsVisible => GetIsTreeVisible();

        private Tree _northTree;
        private Tree _southTree;
        private Tree _westTree;
        private Tree _eastTree;

        public Tree(int id, int height)
        {
            Id = id;
            Height = height;
        }

        public void SetNeighbours(Tree north, Tree south, Tree west, Tree east)
        {
            _northTree = north;
            _southTree = south;
            _westTree = west;
            _eastTree = east;
        }
        
        private int GetTreeScenicScore()
        {
            int visibleTreesAtWest = _westTree?.GetVisibleTreeCountAtWest(Height) ?? 0;
            int visibleTreesAtEast = _eastTree?.GetVisibleTreeCountAtEast(Height) ?? 0;
            int visibleTreesAtSouth = _southTree?.GetVisibleTreeCountAtSouth(Height) ?? 0;
            int visibleTreesAtNorth = _northTree?.GetVisibleTreeCountAtNorth(Height) ?? 0;

            return visibleTreesAtEast * visibleTreesAtNorth * visibleTreesAtSouth * visibleTreesAtWest;
        }

        private int GetVisibleTreeCountAtWest(int heightThreshold)
        {
            if (Height < heightThreshold && _westTree != null)
            {
                return 1 + _westTree?.GetVisibleTreeCountAtWest(heightThreshold) ?? 0;
            }

            return 1;
        }        
        
        private int GetVisibleTreeCountAtEast(int heightThreshold)
        {
            if (Height < heightThreshold && _eastTree != null)
            {
                return 1 + _eastTree?.GetVisibleTreeCountAtEast(heightThreshold) ?? 0;
            }

            return 1;
        }        
        
        private int GetVisibleTreeCountAtSouth(int heightThreshold)
        {
            if (Height < heightThreshold && _southTree != null)
            {
                return 1 + _southTree.GetVisibleTreeCountAtSouth(heightThreshold);
            }

            return 1;
        }        
        
        private int GetVisibleTreeCountAtNorth(int heightThreshold)
        {
            if (Height < heightThreshold && _northTree != null)
            {
                return 1 + _northTree?.GetVisibleTreeCountAtNorth(heightThreshold) ?? 0;
            }

            return 1;
        }

        private bool GetIsTreeVisible()
        {
            return IsTreeVisibleFromNorth() || IsTreeVisibleFromSouth() || IsTreeVisibleFromEast() || IsTreeVisibleFromWest();
        }

        private bool IsTreeVisibleFromWest()
        {
            if (_westTree == null)
            {
                return true;
            }

            return _westTree.GetMaxHeightAtWest() < Height;
        }        
        
        private bool IsTreeVisibleFromEast()
        {
            if (_eastTree == null)
            {
                return true;
            }

            return _eastTree.GetMaxHeightAtEast() < Height;
        }        
        
        private bool IsTreeVisibleFromNorth()
        {
            if (_northTree == null)
            {
                return true;
            }

            return _northTree.GetMaxHeightAtNorth() < Height;
        }        
        
        private bool IsTreeVisibleFromSouth()
        {
            if (_southTree == null)
            {
                return true;
            }

            return _southTree.GetMaxHeightAtSouth() < Height;
        }

        private int GetMaxHeightAtWest()
        {
            int maxHeight = _westTree?.GetMaxHeightAtWest() ?? 0;

            return Math.Max(maxHeight, Height);
        }
        
        private int GetMaxHeightAtEast()
        {
            int maxHeight = _eastTree?.GetMaxHeightAtEast() ?? 0;

            return Math.Max(maxHeight, Height);
        }
        
        private int GetMaxHeightAtNorth()
        {
            int maxHeight = _northTree?.GetMaxHeightAtNorth() ?? 0;

            return Math.Max(maxHeight, Height);
        }
        
        private int GetMaxHeightAtSouth()
        {
            int maxHeight = _southTree?.GetMaxHeightAtSouth() ?? 0;

            return Math.Max(maxHeight, Height);
        }
    }
}