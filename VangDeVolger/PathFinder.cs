using System;
using System.Collections.Generic;
using System.Linq;
using VangDeVolger.Elements.Birds;

namespace VangDeVolger
{
    public class PathFinder
    {
        /// <summary>
        /// Reconstruct path from current to end
        /// </summary>
        /// <param name="current">End position</param>
        /// <returns>List with spots in Optimal Path</returns>
        private List<Spot> _reconstructPath(Spot current)
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

        /// <summary>
        /// Get Next Direction
        /// </summary>
        /// <param name="from">Spot to move from</param>
        /// <param name="to">Closest type to move to</param>
        /// <param name="randomMove">Return Random Move if no path</param>
        /// <returns></returns>
        public Direction? GetNextDirection(Spot from, Type to, bool randomMove = false)
        {
            List<Spot> path = GetOptimalPath(from, to);
            if (path != null)
            {
                return path[0].Neighbors.FirstOrDefault(x => x.Value == path[1]).Key;
            }
            else if (randomMove)
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
        /// <param name="to">Closest type to move to</param>
        /// <returns>List with Optimal Path</returns>
        public List<Spot> GetOptimalPath(Spot from, Type to)
        {
            from.PathCost = 0;
            // Spots to be evaluated
            List<Spot> openSet = new List<Spot> { from };
            // Spots already evaluated
            List<Spot> closedSet = new List<Spot>();

            while (openSet.Count > 0)
            {
                // current = Spot with lowest path cost
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
                if (current.Element != null && current.Element.GetType() == to)
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

                    // Ignore if not a better path
                    else if (newCost >= neighbor.PathCost) continue;

                    neighbor.PathCost = newCost;
                    neighbor.CameFromSpot = current;
                }
            }

            // No solution
            return null;
        }
    }
}
