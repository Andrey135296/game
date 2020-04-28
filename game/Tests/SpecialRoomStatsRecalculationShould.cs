using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	[TestFixture]
	class SpecialRoomStatsRecalculationShould
	{
		[Test]
		public static void TitanInicialised()
		{
			var ship = new TestTitan(Alignment.Player);
			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(5, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);
		}


		[Test]
		public static void RadarIsRecalculated()
		{
			var ship = new TestTitan(Alignment.Player);
			InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[0], 1, ship);
			
            Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);

			Assert.AreEqual(0, ship.Stats.Radar);
			SpecialRoomBonusCalculator.Recalculate(ship);
			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(2, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(1, ship.Stats.Radar);
		}

		[Test]
		public static void HealIsRecalculated()
		{
			var ship = new TestTitan(Alignment.Player);
			InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[4], 1, ship);
	
            Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);

			SpecialRoomBonusCalculator.Recalculate(ship);
			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(1, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);
		}

		[Test]
		public static void DamageMultIsRecalculated()
		{
			var ship = new TestTitan(Alignment.Player);
			InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[5], 1, ship);

			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);

			SpecialRoomBonusCalculator.Recalculate(ship);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(1.1, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);
		}

		[Test]
		public static void ControlIsRecalculated()
		{
			var ship = new TestTitan(Alignment.Player);
			InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[1], 1, ship);

			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);

			SpecialRoomBonusCalculator.Recalculate(ship);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(5, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);
		}

		[Test]
		public static void EngineIsRecalculated()
		{
			var ship = new TestTitan(Alignment.Player);
			InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 1, ship);

			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(0, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);

			SpecialRoomBonusCalculator.Recalculate(ship);
			Assert.AreEqual(4, ship.Stats.CurrentEnergy);
			Assert.AreEqual(1.0, ship.Stats.DamageMultiplier, 0.001);
			Assert.AreEqual(5, ship.Stats.Evasion);
			Assert.AreEqual(5, ship.Stats.FullEnergy);
			Assert.AreEqual(0, ship.Stats.Heal);
			Assert.AreEqual(0, ship.Stats.Radar);
		}

        [Test]
        public static void GeneratorIsRecalculated()
        {
            var ship = new TestTitan(Alignment.Player);
            ship.SpecialRooms[3].Stat.CurrentEnergyLimit = 4;
			SpecialRoomBonusCalculator.Recalculate(ship);
            for (var i = 0; i < ship.SpecialRooms.Count; i++)
            {
                if (ship.SpecialRooms[i].Type != RoomType.Generator)
                    InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[i], 2, ship);
            }
            InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 1, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            var energy = 0;
            foreach (var room in ship.SpecialRooms.Where(room => room.Type!=RoomType.Generator))
                energy += room.Stat.CurrentEnergy;
            Assert.AreEqual(ship.Stats.FullEnergy - ship.Stats.CurrentEnergy, energy);
        }
    }
}
