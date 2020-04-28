using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace game
{
    [TestFixture]
    public class InterfaceCommands_MapCommand_Shold
    {
        [Test]
        public void TestCorrectMove()
        {
			//var p = Path.GetFullPath(@"maps\mapExample.txt");
			//var s = new TestShip(Alignment.Player);
			//var m = Map.LoadFromFile(@"maps\mapExample.txt");
			////bad test
			var ship = new TestShip(Alignment.Player);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"maps\mapExample.txt");
            var map = Map.LoadFromFile(path);

            var gameModel = new GameModel(ship, map);

            Assert.AreEqual(gameModel.map.Nodes[0], gameModel.map.CurrentNode);

            InterfaceCommands.MoveOnMap(gameModel, gameModel.map.Nodes[1]);
            Assert.AreEqual(gameModel.map.Nodes[1], gameModel.map.CurrentNode);
        }

        [Test]
        public void TestMoveToNotNeighbors()
        {
            var ship = new TestShip(Alignment.Player);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"maps\mapExample.txt");
            var map = Map.LoadFromFile(path);

            var gameModel = new GameModel(ship, map);

            Assert.AreEqual(gameModel.map.Nodes[0], gameModel.map.CurrentNode);

            InterfaceCommands.MoveOnMap(gameModel, gameModel.map.Nodes[1]);
            Assert.AreEqual(gameModel.map.Nodes[1], gameModel.map.CurrentNode);

            InterfaceCommands.MoveOnMap(gameModel, gameModel.map.Nodes[2]);
            Assert.AreEqual(gameModel.map.Nodes[1], gameModel.map.CurrentNode);
        }

        [Test]
        public void TestMoveToFight()
        {
            var ship = new TestShip(Alignment.Player);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"maps\mapExample.txt");
            var map = Map.LoadFromFile(path);

            var gameModel = new GameModel(ship, map);

            Assert.AreEqual(gameModel.map.Nodes[0], gameModel.map.CurrentNode);
            Assert.AreEqual(Alignment.Enemy, gameModel.map.Nodes[1].alignment);

            InterfaceCommands.MoveOnMap(gameModel, gameModel.map.Nodes[1]);
            Assert.AreEqual(gameModel.map.Nodes[1], gameModel.map.CurrentNode);
            Assert.IsTrue(gameModel.ship2 is Titan);
        }

        [Test]
        public void TestMoveToNotFight()
        {
            var ship = new TestShip(Alignment.Player);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"maps\mapExample.txt");
            var map = Map.LoadFromFile(path);

            var gameModel = new GameModel(ship, map);

            Assert.AreEqual(gameModel.map.Nodes[0], gameModel.map.CurrentNode);

            InterfaceCommands.MoveOnMap(gameModel, gameModel.map.Nodes[2]);
            Assert.AreEqual(gameModel.map.Nodes[2], gameModel.map.CurrentNode);
            Assert.IsTrue(gameModel.ship2 is null);
        }
    }
}
