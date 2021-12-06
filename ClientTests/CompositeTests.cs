using Client.Composite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class CompositeTests
    {
        [TestMethod]
        public void TestAddGameRoom()
        {
            RoomHub roomHub = new RoomHub("roomHub");
            Room gameRoom = new GameRoom("gameRoom", "password");
            roomHub.AddRoom(gameRoom);
            Assert.IsTrue(gameRoom.Equals(roomHub.GetRoom("gameRoom")));
        }
        [TestMethod]
        public void TestRemoveGameRoom()
        {
            RoomHub roomHub = new RoomHub("roomHub");
            Room gameRoom = new GameRoom("gameRoom", "password");
            roomHub.AddRoom(gameRoom);
            roomHub.RemoveRoom(gameRoom);
            Assert.AreEqual(roomHub.GetRooms().Count, 0);
        }
    }
}