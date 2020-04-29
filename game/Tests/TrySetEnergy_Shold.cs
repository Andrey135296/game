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
    public class InterfaceCommands_TrySetEnergy_Shold
    {
        [Test]
        public void TestSetCorrectEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 2, gameModel.PlayerShip);
            Assert.AreEqual(3, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 1, gameModel.PlayerShip);
            Assert.AreEqual(4, gameModel.PlayerShip.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanRoomLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 3, gameModel.PlayerShip);
            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanShipLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 2, gameModel.PlayerShip);
            Assert.AreEqual(3, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[4], 2, gameModel.PlayerShip);
            Assert.AreEqual(1, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[2], 2, gameModel.PlayerShip);
            Assert.AreEqual(1, gameModel.PlayerShip.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetNegativeEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], -1, gameModel.PlayerShip);
            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetAndUndoEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 2, gameModel.PlayerShip);
            Assert.AreEqual(3, gameModel.PlayerShip.Stats.CurrentEnergy);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 0, gameModel.PlayerShip);
            Assert.AreEqual(5, gameModel.PlayerShip.Stats.CurrentEnergy);

        }
    }

}
