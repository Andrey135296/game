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
    public class InterfaceCommands_TrySetEnergy_Shold
    {
        [Test]
        public void TestSetCorrectEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.Ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 2, gameModel.Ship1);
            Assert.AreEqual(3, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 1, gameModel.Ship1);
            Assert.AreEqual(4, gameModel.Ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanRoomLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.Ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 3, gameModel.Ship1);
            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanShipLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.Ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 2, gameModel.Ship1);
            Assert.AreEqual(3, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[4], 2, gameModel.Ship1);
            Assert.AreEqual(1, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[2], 2, gameModel.Ship1);
            Assert.AreEqual(1, gameModel.Ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetNegativeEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.Ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], -1, gameModel.Ship1);
            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetAndUndoEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-Empty-100,100");
            gameModel.Ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 2, gameModel.Ship1);
            Assert.AreEqual(3, gameModel.Ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.Ship1.SpecialRooms[0], 0, gameModel.Ship1);
            Assert.AreEqual(5, gameModel.Ship1.Stats.CurrentEnergy);

        }
    }

}
