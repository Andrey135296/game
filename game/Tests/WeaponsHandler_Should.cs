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
    public class WeaponsHandler_Shold
    {
        [Test]
        public void TestFireWithAllCoolDownWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);
            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500, ship.Stats.DamageMultiplier);
            Assert.AreEqual(1500, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500, ship.Stats.DamageMultiplier);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500, ship.Stats.DamageMultiplier);
            Assert.AreEqual(500, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 500, ship.Stats.DamageMultiplier);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

        }

        [Test]
        public void TestStatsAfterFireWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(100, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(100, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(100, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(90, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(90, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(190, enemyShip.Stats.HP);
            Assert.AreEqual(90, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(90, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

        }

        [Test]
        public void TestFireWithOverFlowCoolDownWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 2001, ship.Stats.DamageMultiplier);
            Assert.AreEqual(90, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(90, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(190, enemyShip.Stats.HP);
            Assert.AreEqual(1999, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithDamageOverFlow()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);
            enemyShip.Rooms[0].CurrentDurability = 5;
            enemyShip.Stats.HP = 5;
            enemyShip.Crew[0].CurrentHP = 5;
            enemyShip.Crew[1].CurrentHP = 15;

            WeaponsHandler.Tick(ship, enemyShip, 2000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(0, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(0, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(0, enemyShip.Stats.HP);
            Assert.AreEqual(0, enemyShip.Crew[0].CurrentHP);
            Assert.AreEqual(5, enemyShip.Crew[1].CurrentHP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithOfflineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TargetWeapon(ship.Weapons[0], enemyShip.Rooms[0], ship, enemyShip);

            WeaponsHandler.Tick(ship, enemyShip, 2000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOnlineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(1000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(0, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOfflineWeapon()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);

            WeaponsHandler.Tick(ship, enemyShip, 1000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(2000, ship.Weapons[0].TimeLeftToCoolDown);
        }

        [Test]
        public void TestFireWithNoTargetOnlineWeaponOwerflowCooldown()
        {
            var ship = new TestShip(Alignment.Player);
            var enemyShip = new TestShip(Alignment.Enemy);

            InterfaceCommands.TryChangeWeaponState(ship.Weapons[0], ship);

            WeaponsHandler.Tick(ship, enemyShip, 3000, ship.Stats.DamageMultiplier);
            Assert.AreEqual(100, enemyShip.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, enemyShip.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(200, enemyShip.Stats.HP);
            Assert.AreEqual(-1000, ship.Weapons[0].TimeLeftToCoolDown);
        }
    }

}
