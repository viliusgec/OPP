using System;
using NUnit.Framework;
using System.IO;
using Client.Bridge;

namespace ClientTests
{
    public class BridgeTests
    {
        string currentDir;

        [SetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        }

        [TestCase("100")]
        [TestCase("75")]
        [TestCase("50")]
        [TestCase("25")]
        [TestCase("101")]
        public void TestRockSetSkin(string health)
        {
            /*string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;*/
            var ans = RockBlockSkin.SetSkin(health);
            switch (health)
            {
                case "100":
                    Assert.AreEqual(ans, currentDir + @"\Resources\rock1.png");
                    break;
                case "75":
                    Assert.AreEqual(ans, currentDir + @"\Resources\rock2.png");
                    break;
                case "50":
                    Assert.AreEqual(ans, currentDir + @"\Resources\rock3.png");
                    break;
                case "25":
                    Assert.AreEqual(ans, currentDir + @"\Resources\rock4.png");
                    break;
                default:
                    Assert.AreEqual(ans, currentDir + @"\Resources\rock.png");
                    break;
            }
        }

        [TestCase("100")]
        [TestCase("75")]
        [TestCase("50")]
        [TestCase("25")]
        [TestCase("101")]
        public void TestDirtSetSkin(string health)
        {
            var ans = DirtBlockSkin.SetSkin(health);
            switch (health)
            {
                case "100":
                    Assert.AreEqual(ans, currentDir + @"\Resources\dirt2.png");
                    break;
                case "75":
                    Assert.AreEqual(ans, currentDir + @"\Resources\dirt3.png");
                    break;
                case "50":
                    Assert.AreEqual(ans, currentDir + @"\Resources\dirt4.png");
                    break;
                case "25":
                    Assert.AreEqual(ans, currentDir + @"\Resources\dirt5.png");
                    break;
                default:
                    Assert.AreEqual(ans, currentDir + @"\Resources\dirt1.png");
                    break;
            }
        }

        [TestCase("100")]
        [TestCase("75")]
        [TestCase("50")]
        [TestCase("25")]
        [TestCase("101")]
        public void TestSandSetSkin(string health)
        {
            var ans = SandBlockSkin.SetSkin(health);
            switch (health)
            {
                case "100":
                    Assert.AreEqual(ans, currentDir + @"\Resources\sand2.png");
                    break;
                case "75":
                    Assert.AreEqual(ans, currentDir + @"\Resources\sand3.png");
                    break;
                case "50":
                    Assert.AreEqual(ans, currentDir + @"\Resources\sand4.png");
                    break;
                case "25":
                    Assert.AreEqual(ans, currentDir + @"\Resources\sand5.png");
                    break;
                default:
                    Assert.AreEqual(ans, currentDir + @"\Resources\sand1.png");
                    break;
            }
        }
    }
}
