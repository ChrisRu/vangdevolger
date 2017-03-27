using System.Collections.Generic;
using System.Windows.Forms;
using VangDeVolger.Elements;
using VangDeVolger.Elements.Birds;

namespace VangDeVolger.PathFinding
{
    public class PathFinder
    {
        /// <summary>
        /// Path Finding using the Dijkstra algorithm
        /// </summary>
        /// <returns>List with Optimal Path</returns>
        public List<Spot> GetOptimalPath(Spot from)
        {
            List<Spot> optimalPath = new List<Spot>();

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
                if (current.Element is Player)
                {
                    optimalPath.Add(current);
                    Spot next = current;
                    while (next.CameFromSpot != null)
                    {
                        optimalPath.Add(next.CameFromSpot);
                        next = next.CameFromSpot;
                    }
                    return optimalPath;
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
            return optimalPath;
        }
    }
}
