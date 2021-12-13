using Client.Adapter;
using Client.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ClientTests
{
    [TestClass]
    public class AdapterTests
    {
        private readonly string blocks = "mineStronger;mineWide; ;mineWide";
        private readonly Character player;
        private readonly PictureBox box = new();

        [TestMethod]
        public void TestCheckBox()
        {
            BlockCheckerAdaptees temp = new();
            PictureBox pictureBox = new();
            pictureBox.Enabled = true;
            Assert.IsFalse(temp.CheckBox(pictureBox));
            Assert.IsTrue(temp.CheckBox(null));
            pictureBox.Enabled = false;
            Assert.IsTrue(temp.CheckBox(pictureBox));
        }
    }
}