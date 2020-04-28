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
    public class InterfaceCommands_UpgradeRoom_Shold
    {
        [Test]
        public void TestUpgradeWhenNoMoney()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(2, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(0, gameModel.money);
        }

        [Test]
        public void TestUpgradeRoom()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            gameModel.money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(910, gameModel.money);
        }

        [Test]
        public void TestUpgradeRoomWhenLimitReached()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            gameModel.money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(910, gameModel.money);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(4, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(790, gameModel.money);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(4, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(790, gameModel.money);
        }

        [Test]
        public void TestSetEnergyAfterUpgrade()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            gameModel.money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);

            InterfaceCommands.UpgradeRoom(gameModel.ship1.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 3, gameModel.ship1);
            Assert.AreEqual(3, gameModel.ship1.SpecialRooms[0].Stat.CurrentEnergyLimit);
        }
    }
}
