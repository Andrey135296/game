using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public static class PlayerCommands
	{
		public static void MoveCrewMember(CrewMember crewMember, Cell cell, Ship ship)
		{
			if (crewMember.Alignment == Alignment.Player)
				if (!ship.Crew.Any(c => c.Destination == cell) && cell.Stationed == null)
				{
					if (crewMember.Action == CrewAction.Working)
					{
						var room = ship.SpecialRooms.First(r => r.Cells.Contains(crewMember.Cell));
						room.Stat.EmptyWorkingSpaces++;
					}
					crewMember.Destination = cell;
					crewMember.Action = CrewAction.Moving;
				}
		}

		public static void TrySetRoomEnergyConsumption(SpecialRoom room, int energy, Ship ship)
		{
			if (room.Stat.CurrentEnergyLimit>=energy && energy>=0 
				&& ship.Alignment == Alignment.Player 
				&& ship.Stats.CurrentEnergy>=energy-room.Stat.CurrentEnergy
				&& room.Type != RoomType.Generator)
			{
				ship.Stats.CurrentEnergy -= energy - room.Stat.CurrentEnergy;
				room.Stat.CurrentEnergy = energy;
			}
		}

		public static void TryChangeWeaponState(Weapon weapon, Ship ship)
		{
			if (weapon.IsOnline)
				weapon.IsOnline = false;
			else
			{
				var weaponRoom = ship.SpecialRooms.First(r => r.Type == RoomType.Weapon);
				var spaceUsed = ship.Weapons.Sum(w => w.IsOnline ? w.EnergyPrice : 0);
				if (weapon.EnergyPrice <= weaponRoom.Stat.CurrentEnergyLimit - spaceUsed)
					weapon.IsOnline = true;
			}
		}

		public static void TargetWeapon(Weapon weapon, Room target, Ship attacker, Ship attacked)
		{
			if (attacker.Alignment == Alignment.Player && attacked.Alignment == Alignment.Enemy)
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
				if (price <= gameModel.Money)
				{
					room.Stat.CurrentEnergyLimit++;
					gameModel.Money -= price;
				}
			}
		}

		public static void MoveOnMap(GameModel gameModel, Node destination)
		{
			if (gameModel.Map.CurrentNode.Neighbors.Contains(destination))
			{
				gameModel.Map.CurrentNode = destination;
				if (destination.Type == NodeType.Fight)
				{
					gameModel.OtherShip = new Titan(destination.Alignment);
				}
			}
		}
	}
}
