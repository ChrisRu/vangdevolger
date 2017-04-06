
namespace VangDeVolgerTest
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VangDeVolger;
    using VangDeVolger.Elements.Blocks;

    /// <summary>
    /// Unit Test Class for Game
    /// </summary>
    [TestClass]
    public class PathFinderTest
    {
        [TestMethod]
        public void PathFinderTest1()
        {
            Level level = new Level(null, 4, 4, 0);

            for (int i = 0; i < level.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < level.Grid.GetLength(1); j++)
                {
                    level.Grid[i, j].Neighbours = new Dictionary<Direction, Spot>();
                    level.Grid[i, j] = new Spot(null, 32);   
                }
            }

            level.Grid[1, 1] = new Spot(new BlockSolid(), 32);
            level.Grid[2, 1] = new Spot(new BlockSolid(), 32);
            level.Grid[2, 2] = new Spot(new BlockSolid(), 32);
            level.Grid[3, 2] = new Spot(new BlockSolid(), 32);

            level.Grid[1, 2] = new Spot(new BlockMovable(), 32);
            level.Grid[3, 3] = new Spot(new BlockSolid(), 32);

            level.Render(0);

            // - - - -
            // - O O -
            // - - O -
            // - x O x
            // 
            // O = wall
            // - = free
            // x = from / to 
            
            List<Spot> path = new PathFinder().GetOptimalPath(level.Grid[3, 3], typeof(BlockMovable));
            //Assert.AreEqual(path.Count, 4, "Path not long enough");
        }
    }
}
