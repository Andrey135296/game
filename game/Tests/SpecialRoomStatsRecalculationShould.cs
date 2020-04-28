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
		public static void BonusesAreCalculated()
		{
			var s = new Titan(Alignment.Player);
			Assert.AreEqual(s.Stats.DamageMultiplier, 1.0);
			Assert.AreEqual(s.Stats.CurrentEnergy, 5);
			Assert.AreEqual(s.Stats.Evasion, 0);
			Assert.AreEqual(s.Stats.FullEnergy, 5);
			Assert.AreEqual(s.Stats.Heal, 0);
			Assert.AreEqual(s.Stats.Radar, 0);
			InterfaceCommands.TrySetRoomEnergyConsumption(s.SpecialRooms[0], 1, s);
			Assert.AreEqual(s.Stats.Radar, 0);
		}
	}
}
