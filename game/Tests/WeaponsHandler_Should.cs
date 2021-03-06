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
    public class WeaponsHandler_Shold
    {
        [Test]
        public void TestFireWithAllCoolDownWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500);
            Assert.AreEqual(1500, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500);
            Assert.AreEqual(500, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

        }

        [Test]
        public void TestStatsAfterFireWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(90, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(90, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(190, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(90, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(90, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

        }

        [Test]
        public void TestFireWithOverFlowCoolDownWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 2001);
            Assert.AreEqual(90, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(90, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(190, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(1999, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithDamageOverFlow()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            enemyShip.Rooms[0].CurrentDurability = 5;
            enemyShip.Stats.CurrentHP = 5;
            enemyShip.Crew[0].CurrentHP = 5;
            enemyShip.Crew[1].CurrentHP = 15;

            WeaponsHandler.Tick(ship, enemyShip, 2000);
            Assert.AreEqual(0, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(0, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(0, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(0, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(5, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithOfflineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            WeaponsHandler.Tick(ship, enemyShip, 2000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOnlineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(0, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOfflineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOnlineWeaponOwerflowCooldown()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            PlayerCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 3000);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.CurrentHP);
            Assert.AreEqual(-1000, ship.Weapons[0].TimeLeftToCoolDown);
        }
    }

}
