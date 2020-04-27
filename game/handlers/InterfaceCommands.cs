using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	static class InterfaceCommands
	{
		public static void MoveCrewMember(CrewMember crewMember, Cell cell, Ship ship)
		{
			if (crewMember.alignment == Alignment.Player)
				if (!ship.Crew.Any(c => c.Destination == cell) && cell.stationed == null)
				{
					if (crewMember.Action == CrewAction.Working)
					{
						var room = ship.SpecialRooms.Where(r => r.Cells.Contains(crewMember.Cell)).First();
						room.Stat.EmptyWorkingSpaces++;
					}
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

		public static void TargetWeapon(Weapon weapon, Room target, Ship ship1, Ship ship2)
		{
			if (ship1.alignment == Alignment.Player && ship2.alignment == Alignment.Enemy)
			{
				weapon.Target = target;
			}
		}

		public static void UpgradeRoom(SpecialRoom room, GameModel gameModel)
		{
			if (room.Stat.CurrentEnergyLimit < room.Stat.MaxEnergyLimit)
			{
				int price;
				if (room.Type == RoomType.Generator)
					price = (room.Stat.CurrentEnergyLimit + 1) * 15;
				else
					price = (room.Stat.CurrentEnergyLimit + 1) * 30;
				if (price <= gameModel.money)
				{
					room.Stat.CurrentEnergyLimit++;
					gameModel.money -= price;
				}
			}
		}
	}
}
