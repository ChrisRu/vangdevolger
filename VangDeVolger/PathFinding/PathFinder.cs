using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VangDeVolger.Blocks;

namespace VangDeVolger.PathFinding
{
    internal class PathFinder
    {
        private PathFinderBlock[,] _grid;
        private List<PathFinderBlock> _openSet = new List<PathFinderBlock>();
        private List<PathFinderBlock> _closedSet = new List<PathFinderBlock>();

        private readonly PathFinderBlock _to;

        /// <summary>
        /// Initialize PathFinder Class
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public PathFinder(Block[,] grid, Block from, Block to)
        {
            
            _grid = _transformGrid(grid);
            _to = _transformBlock(to);

            _openSet.Add(_transformBlock(from));
        }

        /// <summary>
        /// Path Finding using the A* algorithm
        /// </summary>
        /// <returns>Optimal Path</returns>
        public List<PathFinderBlock> GetOptimalPath()
        {
            var path = new List<PathFinderBlock>();

            while (_openSet.Count > 0)
            {
                var winner = 0;
                for (var i = 0; i < _openSet.Count; i++)
                {
                    if (_openSet[i].F < _openSet[winner].F)
                    {
                        winner = i;
                    }
                }

                var current = _openSet[winner];

                if (current == _to)
                {
                    // DONE
                    var next = current;
                    path.Add(next);
                    
                    while (next.CameFrom != null)
                    {
                        path.Add(next.CameFrom);
                        next = next.CameFrom;
                    }

                    return path;
                }

                _openSet.Remove(current);
                _closedSet.Add(current);

                for (var i = 0; i < current.SiblingBlocks.Count; i++)
                {
                    var sibling = current.SiblingBlocks[i];
                    if (!_closedSet.Contains(sibling) && sibling != null) {

                        var tempG = current.G + 1;

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
            }
            // No solution
            return path;
        }

        private PathFinderBlock[,] _transformGrid(Block[,] grid)
        {
            PathFinderBlock[,] newGrid = new PathFinderBlock[grid.GetLength(0), grid.GetLength(1)];
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++) {
                    var block = grid[y, x];
                    if (block == null)
                    {
                        block = new BlockSolid(x, y);
                    }
                    newGrid[block.X, block.Y] = _transformBlock(block);
                }
                
            }
            return newGrid;
        }

        private PathFinderBlock _transformBlock(Block block)
        {
            var x = block.X;
            var y = block.Y;
            var siblings = new List<PathFinderBlock>();

            if (x < _grid.GetLength(1))
            {
                siblings.Add(_grid[x + 1, y]);
            }
            if (x > 0)
            {
                siblings.Add(_grid[x - 1, y]);
            }
            if (y < _grid.GetLength(0))
            {
                siblings.Add(_grid[x, y + 1]);
            }
            if (y > 0)
            {
                siblings.Add(_grid[x, y - 1]);
            }

            return new PathFinderBlock(block, siblings);
        }

        private int Heuristic(PathFinderBlock from)
        {
            // Manhattan distance
            var x = Math.Abs(from.Block.X - _to.Block.X);
            var y = Math.Abs(from.Block.Y + _to.Block.Y);
            return x + y;
        }
    }
}
