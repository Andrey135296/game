using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	static class SpecialRoomBonusCalculator
	{
		public static void Recalculate(Ship ship)
		{
			if (ship == null)
				return;
			int radar = 0;
			int evasion = 0;
			int heal = 0;
			int energy = 0;
			int maxEnergy = 0;
			double damegeMult = 1.0;
			foreach (var room in ship.SpecialRooms)
			{
				switch (room.Type)
				{
					case RoomType.Radar:
						radar += room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces;
						evasion += radar * 2;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Control:
						evasion += 5 * (room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces);
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Engine:
						evasion += 5 * (room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces);
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Living:
						heal += room.Stat.CurrentEnergy;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Generator:
						maxEnergy = room.Stat.CurrentEnergyLimit;
						energy += maxEnergy;
						break;
					case RoomType.Weapon:
						damegeMult += 0.1 * (room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces);
						energy -= room.Stat.CurrentEnergy;
						break;
				}
			}
			ship.Stats.CurrentEnergy = energy;
			ship.Stats.DamageMultiplier = damegeMult;
			ship.Stats.FullEnergy = maxEnergy;
			ship.Stats.Radar = radar;
			ship.Stats.Evasion = evasion;
			ship.Stats.Heal = heal;
		}
	}
}
