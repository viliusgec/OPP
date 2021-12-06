﻿using Client.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class StrategyTests
    {
        Algorithm strategy;
        [TestMethod]
        public void TestJump()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 10, 5 };
            strategy = new Jump(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMine()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 10, 15 };
            strategy = new Mine(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMineLeft()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 10, 10 };
            strategy = new MineLeft(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMineRight()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 10, 10 };
            strategy = new MineRight(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMoveLeft()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 5, 10 };
            strategy = new MoveLeft(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMoveRight()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 15, 10 };
            strategy = new MoveRight(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMoveUpLeft()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 5, 5 };
            strategy = new MoveUpLeft(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        [TestMethod]
        public void TestMoveUpRight()
        {
            int[] temp = { 0, 0 };
            int[] answer = { 15, 5 };
            strategy = new MoveUpRight(10, 10, 5, 5);
            temp = strategy.Behave(10, 10, 5, 5);
            CollectionAssert.AreEqual(answer, temp);
        }
        /*        [TestMethod]
                public void TestSendCoordinates()
                {
                    PictureBox picture1 = new();
                    PictureBox picture2 = new();
                    Label scorelabel = new();
                    HubConnection connection = new();
                    Room room = new("labas", "labase"); 
                    MapBuilder MapBuilder = new();
                    FormsEditor editor = new(picture1, picture2, scorelabel);
                    object sender = new();
                    KeyEventArgs e;
                    Character player = new();
                    Client.Map.MapBase map = new();
                    Movement movement;
                    movement = new Movement(connection, room.GetName());
                    movement.SendBoxCoordinates(e, editor, map, player, MapBuilder);
                    Assert.AreEqual(1, 1);
                }*/
    }
}
