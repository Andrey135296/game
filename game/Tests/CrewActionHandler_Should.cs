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
    public class CrewActionHandler_Should
    {
        [Test]
        public void TestStartWorkingAndNotWorking()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[0].Action);
            Assert.AreEqual(CrewAction.Working, ship.Crew[1].Action);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[3].Action);
            Assert.AreEqual(CrewAction.Working, ship.Crew[4].Action);
        }

        [Test]
        public void TestMove()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
        }

        [Test]
        public void TestCrewStatesAfterMoving()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Destination);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(null, ship.Crew[2].Destination);
        }


        [Test]
        public void TestCrewActionsAfterMovingToFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
        }

        [Test]
        public void TestCrewActionsAfterMovingToNotFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[2].Action);
        }

        [Test]
        public void TestRoomStatesAfterMovingFromNotWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            Assert.AreEqual(0, ship.SpecialRooms[1].Stat.EmptyWorkingSpaces);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[1].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestRoomStatesAfterMovingFromWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            Assert.AreEqual(0, ship.SpecialRooms[0].Stat.EmptyWorkingSpaces);

            InterfaceCommands.MoveCrewMember(ship.Crew[1], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(1, ship.SpecialRooms[0].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestRoomStatesAfterMovingToNotFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(1, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestRoomStatesAfterMovingToFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(1, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);

            InterfaceCommands.MoveCrewMember(ship.Crew[3], ship.Cells[10], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[4].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestWayToDistination()
        {
            var ship = new TestShip(Alignment.Player);
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[4], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[10], ship.Crew[4].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[11], ship.Crew[4].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[3], ship.Crew[4].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
        }

        [Test]
        public void TestTwoMovingCrewMembers()
        {
            var ship = new TestShip(Alignment.Player);
            CrewActionsHandler.TickCrew(ship);

            InterfaceCommands.MoveCrewMember(ship.Crew[4], ship.Cells[2], ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[8], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[10], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[11], ship.Crew[2].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[11], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[10], ship.Crew[2].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[3], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[9], ship.Crew[2].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[8], ship.Crew[2].Cell);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[8], ship.Crew[2].Cell);
        }

    }

    [TestFixture]
    public class InterfaceCommands_TrySetEnergy_Shold
    {
        [Test]
        public void TestSetCorrectEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 2, gameModel.ship1);
            Assert.AreEqual(3, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 1, gameModel.ship1);
            Assert.AreEqual(4, gameModel.ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanRoomLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 3, gameModel.ship1);
            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetMoreEnergyThanShipLimit()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 2, gameModel.ship1);
            Assert.AreEqual(3, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[4], 2, gameModel.ship1);
            Assert.AreEqual(1, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[2], 2, gameModel.ship1);
            Assert.AreEqual(1, gameModel.ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetNegativeEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], -1, gameModel.ship1);
            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);
        }

        [Test]
        public void TestSetAndUndoEnergy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);
            var gameModel = new GameModel(ship, "Player-empty-100,100");
            gameModel.ship2 = enemyShip;
            GameTick.Tick(gameModel);

            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 2, gameModel.ship1);
            Assert.AreEqual(3, gameModel.ship1.Stats.CurrentEnergy);

            InterfaceCommands.TrySetRoomEnergyConsumption(gameModel.ship1.SpecialRooms[0], 0, gameModel.ship1);
            Assert.AreEqual(5, gameModel.ship1.Stats.CurrentEnergy);

        }
    }

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

    [TestFixture]
    public class InterfaceCommands_WeaponsCommand_Shold
    {
        [Test]
        public void TestCorrectConnectWeapon()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);
        }

        [Test]
        public void TestCorrectFullConnectWeapon()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[1], ship);
            Assert.AreEqual(true, ship.Weapons[1].IsOnline);
        }

        [Test]
        public void TestCorrectTwoConnectWeapon()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);
            Assert.AreEqual(false, ship.Weapons[3].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[3], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);
            Assert.AreEqual(true, ship.Weapons[3].IsOnline);
        }

        [Test]
        public void TestConnectAndUnconnect()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));
        }

        [Test]
        public void TestConnectAndUnconnectTwoWeapons()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);
            Assert.AreEqual(false, ship.Weapons[3].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[3], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);
            Assert.AreEqual(true, ship.Weapons[3].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(false, ship.Weapons[0].IsOnline);
            Assert.AreEqual(true, ship.Weapons[3].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[3], ship);
            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));
        }

        [Test]
        public void TestTooBigWeaponConnect()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[2], ship);
            Assert.AreEqual(false, ship.Weapons[2].IsOnline);
        }

        [Test]
        public void TestTooBigWeaponConnect2()
        {
            var ship = new TestShip(Alignment.Player);

            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[1], ship);
            Assert.AreEqual(false, ship.Weapons[1].IsOnline);
        }

        [Test]
        public void TestTargetWeaponToEnemy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
        }

        [Test]
        public void TestTargetWeaponNotToEnemy()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Neutral);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(null, ship.Weapons[0].Target);
        }

        [Test]
        public void TestTargetTwoWeaponToOneRoom()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(null, ship.Weapons[1].Target);

            InterfaceCommands.TargetWeapon(ship.Weapons[1], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[1].Target);
        }

        [Test]
        public void TestTargetTwoWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(null, ship.Weapons[1].Target);

            InterfaceCommands.TargetWeapon(ship.Weapons[1], enemyShip.Rooms[1], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(enemyShip.Rooms[1], ship.Weapons[1].Target);
        }

        [Test]
        public void TestRetargetWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[1], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[1], ship.Weapons[0].Target);
        }

        [Test]
        public void TestSaveTargetAfterConnectAndDisconectWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            Assert.IsTrue(ship.Weapons.All(c => c.Target == null));
            Assert.IsTrue(ship.Weapons.All(c => c.IsOnline == false));

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(false, ship.Weapons[0].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(true, ship.Weapons[0].IsOnline);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            Assert.AreEqual(enemyShip.Rooms[0], ship.Weapons[0].Target);
            Assert.AreEqual(false, ship.Weapons[0].IsOnline);

        }
    }

    [TestFixture]
    public class InterfaceCommands_MapCommand_Shold
    {
        [Test]
        public void TestCorrectMove()
        {
			//var ship = new TestShip(Alignment.Player);
			//var map = Map.LoadFromFile(@"maps\mapExample.txt");
			//bad test
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
    }
}
