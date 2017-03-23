using System.Collections.Generic;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;

namespace VangDeVolger.PathFinding
{
    public class PathFinder
    {
        private readonly List<Spot> _openSet;
        private readonly List<Spot> _closedSet;
        private readonly Spot _to;

        /// <summary>
        /// Initialize PathFinder Class
        /// </summary>
        /// <param name="grid">Current Grid of Elements</param>
        /// <param name="from">Position of Enemy Bird</param>
        /// <param name="to">Position of Player Bird</param>
        public PathFinder(Spot[,] grid, Spot from, Spot to)
        {
            _openSet = new List<Spot>();
            _closedSet = new List<Spot>();

            foreach (Spot spot in grid)
            {
                if (to == spot)
                {
                    _to = spot;
                }
                else if (from == spot)
                {
                    _openSet.Add(spot);
                }
            }
        }

        /// <summary>
        /// Path Finding using the A* algorithm
        /// </summary>
        /// <returns>List with Optimal Path</returns>
        public List<Spot> GetOptimalPath()
        {
            List<Spot> path = new List<Spot>();

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

                Spot current = _openSet[winner];

                if (current == _to)
                {
                    // DONE

                    Spot next = current;
                    path.Add(next);
                    
                    while (next.CameFromSpot != null)
                    {
                        path.Add(next);
                        next = next.CameFromSpot;
                    }

                    return path;
                }

                _openSet.Remove(current);
                _closedSet.Add(current);

                foreach (KeyValuePair<Direction, Spot> neighbor in current.Neighbors)
                {
                    Spot spot = neighbor.Value;

                    if (_closedSet.Contains(spot)) continue;

                    if (spot.Element != null && !(spot.Element is Bird)) continue;

                    int tempG = current.G + 1;

                    if (_openSet.Contains(spot))
                    {
                        if (tempG < spot.G)
                        {
                            spot.G = tempG;
                        }
                    }
                    else
                    {
                        spot.G = tempG;
                        _openSet.Add(spot);
                    }

                    spot.H = Heuristic();
                    spot.F = spot.G + spot.H;
                    spot.CameFromSpot = current;
                }
            }
            // No solution
            return path;
        }

        /// <summary>
        /// Get Estimated Distance (Heuristic)
        /// </summary>
        /// <returns>Average block distance</returns>
        private int Heuristic() => 32;
    }
}
