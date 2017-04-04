using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VangDeVolger;

namespace VangDeVolgerTest
{
    [TestClass]
    public class UnitTest1
    {
        public Game game { get; set; }

        public UnitTest1()
        {
            this.game = new Game();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(game.GameLevel.Grid.GetLength(0) > 0);
        }

        [TestMethod]
        public void TestMethod2()
        {

        }
    }
}
