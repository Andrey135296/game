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
			var ship = new Titan(Alignment.Player);
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
			var ship = new Titan(Alignment.Player);
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
			var ship = new Titan(Alignment.Player);
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
			var ship = new Titan(Alignment.Player);
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
			var ship = new Titan(Alignment.Player);
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
			var ship = new Titan(Alignment.Player);
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
	}
}
