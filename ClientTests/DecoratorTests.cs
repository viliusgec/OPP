using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Client.Decorator;
using System.IO;
using System;

namespace ClientTests
{
    [TestClass]
    public class DecoratorTests
    {
        Character player;

        [TestMethod]
        public void TestMineWide()
        {
            player = new MineWide(player);
            player.Mine("");

            Assert.AreEqual(player.Mine(""), "mine,mineWide");
        }

        [TestMethod]
        public void TestMineDeep()
        {
            player = new MineDeep(player);
            player.Mine("");

            Assert.AreEqual(player.Mine(""), "mine,mineDeep");
        }

        [TestMethod]
        public void TestMineStronger()
        {
            player = new MineStronger(player);
            player.Mine("");

            Assert.AreEqual(player.Mine(""), "mine,mineStronger");
        }

        [TestMethod]
        public void TestMineNormal()
        {
            player.Mine("");

            Assert.AreEqual(player.Mine(""), "mine");
        }
    }
}
