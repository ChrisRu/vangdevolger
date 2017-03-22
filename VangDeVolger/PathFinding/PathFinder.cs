using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;

namespace VangDeVolger.PathFinding
{
    internal class PathFinder
    {
        /*
        private readonly List<PathFinderElement> _openSet = new List<PathFinderElement>();
        private readonly List<PathFinderElement> _closedSet = new List<PathFinderElement>();

        private readonly PathFinderElement _to;

        /// <summary>
        /// Initialize PathFinder Class
        /// </summary>
        /// <param name="grid">Current Grid of Elements</param>
        /// <param name="from">Position of Enemy Bird</param>
        /// <param name="to">Position of Player Bird</param>
        public PathFinder(Element[,] grid, Spot from, Spot to)
        {
            PathFinderElement[,] pathFinderGrid = _addSiblings(_transformGrid(grid));
            foreach (PathFinderElement item in pathFinderGrid)
            {
                if (to.X == item.X && to.Y == item.Y)
                {
                    _to = item;
                }
                else if (from.X == item.X && from.Y == item.Y)
                {
                    _openSet.Add(item);
                }
            }
        }

        /// <summary>
        /// Path Finding using the A* algorithm
        /// </summary>
        /// <returns>List with Optimal Path</returns>
        public List<Coordinates> GetOptimalPath()
        {
            List<Coordinates> path = new List<Coordinates>();

            while (_openSet.Count > 0)
            {
                int winner = 0;
                for (int i = 0; i < _openSet.Count; i++)
                {
                    if (_openSet[i].F < _openSet[winner].F)
                    {
                        winner = i;
                    }
                }

                PathFinderElement current = _openSet[winner];

                if (current == _to)
                {
                    // DONE

                    PathFinderElement next = current;
                    path.Add(new Coordinates(next.X, next.Y));
                    
                    while (next.CameFrom != null)
                    {
                        path.Add(new Coordinates(next.CameFrom.X, next.CameFrom.Y));
                        next = next.CameFrom;
                    }

                    return path;
                }

                _openSet.Remove(current);
                _closedSet.Add(current);

                foreach (PathFinderElement sibling in current.SiblingBlocks)
                {
                    if (_closedSet.Contains(sibling)) continue;

                    if (sibling.Element != null && !(sibling.Element is Bird)) continue;

                    int tempG = current.G + 1;

                    if (_openSet.Contains(sibling))
                    {
                        if (tempG < sibling.G)
                        {
                            sibling.G = tempG;
                        }
                    }
                    else
                    {
                        sibling.G = tempG;
                        _openSet.Add(sibling);
                    }

                    sibling.H = Heuristic(sibling);
                    sibling.F = sibling.G + sibling.H;
                    sibling.CameFrom = current;
                }
            }
            // No solution
            return path;
        }

        /// <summary>
        /// Transform Blocks to PathFinderBlocks
        /// </summary>
        /// <param name="grid">Block Grid</param>
        /// <returns></returns>
        private static PathFinderElement[,] _transformGrid(Element[,] grid)
        {
            PathFinderElement[,] newGrid = new PathFinderElement[grid.GetLength(0), grid.GetLength(1)];
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++) {
                    newGrid[x, y] = _transformBlock(grid[x, y], x, y);
                }
                
            }
            return newGrid;
        }

        /// <summary>
        /// Transform Block to PathFinderBlock
        /// </summary>
        /// <param name="block">Block to be converted</param>
        /// <param name="x">Position X of the Block</param>
        /// <param name="y">Position Y of the Block</param>
        /// <returns>PathFinderBlock</returns>
        private static PathFinderElement _transformBlock(Element block, int x, int y) => new PathFinderElement(block, x, y);

        private PathFinderElement[,] _addSiblings(PathFinderElement[,] grid)
        {
            PathFinderElement[,] newGrid = new PathFinderElement[grid.GetLength(0), grid.GetLength(1)];
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    PathFinderElement block = grid[x, y];
                    block = _addSibling(grid, block);
                    newGrid[x, y] = block;
                }
            }
            return newGrid;
        }

        /// <summary>
        /// Adds Sibling to PathFinderBlock
        /// </summary>
        /// <param name="grid">PathFinderBlock grid</param>
        /// <param name="block">Block to add Siblings to</param>
        /// <returns></returns>
        private static PathFinderElement _addSibling(PathFinderElement[,] grid, PathFinderElement block)
        {
            int x = block.X;
            int y = block.Y;

            List<PathFinderElement> siblings = new List<PathFinderElement>();

            if (x < grid.GetLength(0) - 1)
            {
                siblings.Add(grid[x + 1, y]);
            }
            if (x > 0)
            {
                siblings.Add(grid[x - 1, y]);
            }
            if (y < grid.GetLength(0) - 1)
            {
                siblings.Add(grid[x, y + 1]);
            }
            if (y > 0)
            {
                siblings.Add(grid[x, y - 1]);
            }

            block.SiblingBlocks = siblings;

            return block;
        }

        /// <summary>
        /// Get Estimated Distance (Heuristic)
        /// </summary>
        /// <param name="from">Block from where to calculate the Heuristic</param>
        /// <returns>Average block distance</returns>
        private int Heuristic(PathFinderElement from)
        {
            // Manhattan distance
            int x = Math.Abs(from.X - _to.X);
            int y = Math.Abs(from.Y - _to.Y);
            return x + y;
        }*/
    }
}
