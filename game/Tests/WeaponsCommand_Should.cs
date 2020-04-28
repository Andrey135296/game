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

}
