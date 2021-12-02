using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Client.Decorator;
using Client.Adapter;
using Moq;
using Client.PictureBoxBuilder;

namespace ClientTests
{
   [TestClass]
    public class AdapterTests
    {
        string blocks = "mineStronger;mineWide; ;mineWide";
        Character player;
        PictureBox box = new();

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

        [TestMethod]
        public void TestAdapterConst()
        {
            BlockCheckerAdapter a = new(10, 1, 1, new Client.FormsEditor(box, box, new Label()), new Client.Map.MapBase(10,10), null, player, null, "Room");
            //var ans = a.check_if_block_exists();
            //Assert.IsFalse(ans);
            Assert.IsNotNull(a);
        }
    }
}