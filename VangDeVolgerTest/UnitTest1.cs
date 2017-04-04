using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VangDeVolger;

namespace VangDeVolgerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Game game = new Game();
            Assert.IsTrue(game.GameLevel.Grid.GetLength(0) > 0);
        }
    }
}
