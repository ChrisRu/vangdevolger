
using System.Windows.Forms;

namespace VangDeVolger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elements.Birds;

    /// <summary>
    /// PathFinder Class to get the optimal path from a to b on the grid
    /// </summary>
    public class PathFinder
    {
        /// <summary>
        /// Get Next Direction
        /// </summary>
        /// <param name="from">Spot to move from</param>
        /// <param name="to">(Nearest) Type to move to</param>
        /// <param name="randomMove">Random Move if no path</param>
        /// <returns>Next Direction from path</returns>
        public Direction? GetNextDirection(Spot from, Type to, bool randomMove = false)
        {
            List<Spot> path = this.GetOptimalPath(from, to);
            if (path != null && path.Count > 1)
            {
                var direction = path[0].Neighbors.FirstOrDefault(x => x.Value == path[1]).Key;
                if (from.Neighbors[direction].Element != null && !(from.Neighbors[direction].Element is Player))
                {
                    randomMove = true;
                }
                else
                {
                    return direction;
                }
            }
            if (randomMove)
            {
                List<Direction> directions = new List<Direction>(from.Neighbors.Keys.Where(key => from.Neighbors[key].Element == null));
                if (directions.Count > 0)
                {
                    return directions.OrderBy(x => Guid.NewGuid()).First();
                }
            }

            return null;
        }

        /// <summary>
        /// Path Finding using the Dijkstra algorithm
        /// </summary>
        /// <param name="from">Spot to move from</param>
        /// <param name="to">(Nearest) Type to move to</param>
        /// <returns>List with Optimal Path</returns>
        public List<Spot> GetOptimalPath(Spot from, Type to)
        {
            // Spots to be evaluated
            List<Spot> openSet = new List<Spot> { from };

            // Spots already evaluated
            List<Spot> closedSet = new List<Spot>();

            while (openSet.Count > 0)
            {
                // Spot with lowest path cost
                int winner = 0;
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].PathCost < openSet[winner].PathCost)
                    {
                        winner = i;
                    }
                }
                Spot current = openSet[winner];

                // Found path
                if (current.Element?.GetType() == to)
                {
                    return _reconstructPath(current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Spot neighbor in current.Neighbors.Values)
                {
                    // Ignore if already evaluated
                    if (closedSet.Contains(neighbor)) continue;

                    // Ignore if invalid block to move to
                    if (neighbor.Element != null && !(neighbor.Element is Bird)) continue;

                    // cost + distance
                    int newCost = current.PathCost + 1;

                    // New node found
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else if (newCost >= neighbor.PathCost) continue;
                    
                    // Better path found
                    neighbor.PathCost = newCost;
                    neighbor.CameFromSpot = current;
                }
            }

            // No solution
            return null;
        }

        /// <summary>
        /// Reconstruct path from current to end
        /// </summary>
        /// <param name="current">End position</param>
        /// <returns>List with spots in Optimal Path</returns>
        private static List<Spot> _reconstructPath(Spot current)
        {
            List<Spot> path = new List<Spot> { current };
            Spot next = current;
            while (next.CameFromSpot != null)
            {
                next = next.CameFromSpot;
                if (path.Contains(next)) break;
                path.Add(next);
            }
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }
    }
}
