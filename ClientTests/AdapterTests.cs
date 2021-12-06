using Client.Adapter;
using Client.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ClientTests
{
    [TestClass]
    public class AdapterTests
    {
#pragma warning disable CS0414 // The field 'AdapterTests.blocks' is assigned but its value is never used
        string blocks = "mineStronger;mineWide; ;mineWide";
#pragma warning restore CS0414 // The field 'AdapterTests.blocks' is assigned but its value is never used
#pragma warning disable CS0169 // The field 'AdapterTests.player' is never used
        Character player;
#pragma warning restore CS0169 // The field 'AdapterTests.player' is never used
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
            //BlockCheckerAdapter a = new(10, 1, 1, new Client.FormsEditor(box, box, new Label()), new Client.Map.MapBase(10,10), null, player, null, "Room");
            //var ans = a.check_if_block_exists();
            //Assert.IsFalse(ans);
            // Assert.IsNotNull(a);
        }
    }
}