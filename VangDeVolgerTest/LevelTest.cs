
namespace VangDeVolgerTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VangDeVolger;
    using VangDeVolger.Elements;

    /// <summary>
    /// Unit Test Class for Game
    /// </summary>
    [TestClass]
    public class LevelTest
    {

        // Level.Paused

        [TestMethod]
        public void TestPauseEnemy1()
        {
            Level level = new Level(null, 16, 16, 0) { Paused = false };
            Assert.IsTrue(level.Enemy.MoveTimer.Enabled, "Enemy doesn't execute move method");
        }

        [TestMethod]
        public void TestPauseEnemy2()
        {
            Level level = new Level(null, 16, 16, 0) { Paused = true };
            Assert.IsFalse(level.Enemy.MoveTimer.Enabled, "Enemy doesn't stop executing move method");
        }

        // Level.GetRandomGrid

        [TestMethod]
        public void TestRandomGrid1()
        {
            int gridSizeX = 14;
            int gridSizeY = 16;
            Spot[,] grid = new Level(null, gridSizeX, gridSizeY, 0).GetRandomGrid(gridSizeX, gridSizeY);
            Assert.IsTrue(grid.GetLength(0) == gridSizeX && grid.GetLength(1) == gridSizeY, "Random Grid is incorrect");
        }

        [TestMethod]
        public void TestRandomGrid2()
        {
            int gridSizeX = 14;
            int gridSizeY = 6;
            Spot[,] grid = new Level(null, gridSizeX, gridSizeY, 0).GetRandomGrid(gridSizeX, gridSizeY);
            Assert.IsTrue(grid.GetLength(0) == gridSizeX && grid.GetLength(1) == gridSizeY, "Random Grid is incorrect");
        }

        [TestMethod]
        public void TestRandomGrid3()
        {
            int gridSizeX = 10;
            int gridSizeY = 10;
            Spot[,] grid = new Level(null, gridSizeX, gridSizeY, 0).GetRandomGrid(gridSizeX, gridSizeY);
            Assert.IsTrue(grid.GetLength(0) == gridSizeX && grid.GetLength(1) == gridSizeY, "Random Grid is incorrect");
        }

        [TestMethod]
        public void TestRandomElement1()
        {
            Level level = new Level(null, 16, 16, 0);
            Element element = level.GetRandomElement(10);
            foreach (KeyValuePair<int, Type> randType in level.RandomElements)
            {
                if (element == null || randType.Value == element.GetType())
                {
                    Assert.IsTrue(true);
                    return;
                }
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestRandomElement2()
        {
            Level level = new Level(null, 16, 16, 0);
            Element element = level.GetRandomElement(100);
            foreach (KeyValuePair<int, Type> randType in level.RandomElements)
            {
                if (element == null || randType.Value == element.GetType())
                {
                    Assert.IsTrue(true);
                    return;
                }
            }
            Assert.Fail();
        }
    }
}
