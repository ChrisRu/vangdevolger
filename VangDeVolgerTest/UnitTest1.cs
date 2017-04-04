
namespace VangDeVolgerTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VangDeVolger;

    /// <summary>
    /// Unit Test Class for Game
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public Game Game { get; set; }

        /// <summary>
        /// Initialize Unit Test Class
        /// </summary>
        public UnitTest1()
        {
            this.Game = new Game();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(this.Game.GameLevel.Grid.GetLength(0) > 0);
        }

        [TestMethod]
        public void TestMethod2()
        {

        }
    }
}
