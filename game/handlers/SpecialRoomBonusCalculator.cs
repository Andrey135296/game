using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public static class SpecialRoomBonusCalculator
	{
		private readonly static Random random = new Random();

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
				var roomLevel = 0;
				switch (room.Type)
				{
					case RoomType.Radar:
						roomLevel = room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						radar += roomLevel;
						evasion += radar * 2;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Control:
						roomLevel = room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						evasion += 5 * roomLevel;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Engine:
						roomLevel = room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						evasion += 5 * roomLevel;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Living:
						roomLevel = room.Stat.CurrentEnergy;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						heal += roomLevel;
						energy -= room.Stat.CurrentEnergy;
						break;
					case RoomType.Generator:
						roomLevel = room.Stat.CurrentEnergyLimit;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						maxEnergy += roomLevel;
						energy += maxEnergy;
						break;
					case RoomType.Weapon:
						roomLevel = room.Stat.CurrentEnergy + room.Stat.MaxWorkingSpaces - room.Stat.EmptyWorkingSpaces;
						roomLevel = roomLevel * room.CurrentDurability / room.MaxDurability;
						damegeMult += 0.1 * (roomLevel);
						energy -= room.Stat.CurrentEnergy;
						break;
				}
			}
			if (energy < 0)
			{
				while (energy < 0)
				{
					var vr = ship.SpecialRooms.Where(r => r.Stat.CurrentEnergy > 0 && r.Type != RoomType.Generator).ToList();
					var room = vr[random.Next(0, vr.Count)];
					PlayerCommands.TrySetRoomEnergyConsumption(room, room.Stat.CurrentEnergy - 1, ship);
					energy++;
				}
				Recalculate(ship);
				return;
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
