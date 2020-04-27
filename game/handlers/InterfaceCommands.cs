using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.handlers
{
	static class InterfaceCommands
	{
		public static void MoveCrewMember(CrewMember crewMember, Cell cell, Ship ship)
		{
			if (crewMember.alignment == Alignment.Player)
				if (!ship.Crew.Any(c => c.Destination == cell) && cell.stationed == null)
				{
					crewMember.Destination = cell;
					crewMember.Action = CrewAction.Moving;
				}
		}

		public static void TrySetRoomEnergyConsumption(SpecialRoom room, int energy, Ship ship)
		{
			if (room.Stat.CurrentEnergyLimit>=energy && energy>=0 
				&& ship.alignment == Alignment.Player 
				&& ship.Stats.CurrentEnergy>=energy-room.Stat.CurrentEnergy)
			{
				room.Stat.CurrentEnergy = energy;
				SpecialRoomBonusCalculator.Recalculate(ship);
			}
		}

		public static void TryChangeWeaponState(Weapon weapon, Ship ship)
		{
			if (weapon.IsOnline)
				weapon.IsOnline = false;
			else
			{
				var weaponRoom = ship.SpecialRooms.Where(r => r.Type == RoomType.Weapon).First();
				var spaceUsed = ship.Weapons.Sum(w => w.IsOnline ? w.EnergyPrice : 0);
				if (weapon.EnergyPrice <= weaponRoom.Stat.CurrentEnergyLimit)
					weapon.IsOnline = true;
			}
		}
	}
}
