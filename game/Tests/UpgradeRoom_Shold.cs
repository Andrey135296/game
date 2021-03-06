﻿using System;
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
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(2, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(0, gameModel.Money);
        }

        [Test]
        public void TestUpgradeRoom()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            gameModel.Money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(910, gameModel.Money);
        }

        [Test]
        public void TestUpgradeRoomWhenLimitReached()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            gameModel.Money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(910, gameModel.Money);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(4, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(790, gameModel.Money);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(4, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
            Assert.AreEqual(790, gameModel.Money);
        }

        [Test]
        public void TestSetEnergyAfterUpgrade()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.OtherShip = enemyShip;
            gameModel.Money = 1000;
            GameTick.Tick(gameModel);

            Assert.AreEqual(2, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);

            PlayerCommands.UpgradeRoom(gameModel.PlayerShip.SpecialRooms[0], gameModel);
            Assert.AreEqual(3, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);

            PlayerCommands.TrySetRoomEnergyConsumption(gameModel.PlayerShip.SpecialRooms[0], 3, gameModel.PlayerShip);
            Assert.AreEqual(3, gameModel.PlayerShip.SpecialRooms[0].Stat.CurrentEnergyLimit);
        }
    }
}
