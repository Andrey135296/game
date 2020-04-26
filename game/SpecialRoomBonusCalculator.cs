using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class SpecialRoomBonusCalculator
	{
		public void RecalculateBonuses(Ship ship)
		{
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
						radar += room.RoomEnergyStat.CurrentEnergy + room.WorkingSpaces - room.EmptyWorkingSpaces;
						evasion += radar * 2;
						energy -= room.RoomEnergyStat.CurrentEnergy;
						break;
					case RoomType.Control:
						evasion += 5 * (room.RoomEnergyStat.CurrentEnergy + room.WorkingSpaces - room.EmptyWorkingSpaces);
						energy -= room.RoomEnergyStat.CurrentEnergy;
						break;
					case RoomType.Engine:
						evasion += 5 * (room.RoomEnergyStat.CurrentEnergy + room.WorkingSpaces - room.EmptyWorkingSpaces);
						energy -= room.RoomEnergyStat.CurrentEnergy;
						break;
					case RoomType.Living:
						heal += room.RoomEnergyStat.CurrentEnergy;
						energy -= room.RoomEnergyStat.CurrentEnergy;
						break;
					case RoomType.Generator:
						maxEnergy = room.RoomEnergyStat.CurrentEnergyLimit;
						energy += maxEnergy;
						break;
					case RoomType.Weapon:
						damegeMult += 0.1 * (room.RoomEnergyStat.CurrentEnergy + room.WorkingSpaces - room.EmptyWorkingSpaces);
						energy -= room.RoomEnergyStat.CurrentEnergy;
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
