using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class Titan : Ship
	{
		public Titan(Alignment alignment)
		{
			this.Alignment = alignment;
			Stats = new ShipStat(500);
			GenerateCells();
			GenerateCrew();
			GenerateRooms();
			GenerateSpecialRooms(GenerateSpecialRoomsStat());
			GenerateWeapons();
			SpecialRoomBonusCalculator.Recalculate(this);
		}

		private void GenerateWeapons()
		{
			Weapons = new List<Weapon>
			{
				new LaserM0(),
				new LaserM0()
			};
		}

		private void GenerateCells()
		{
			Cells = new List<Cell>();
			for (int i = 6; i < 8; i++)
				Cells.Add(new Cell(i, 0));
			for (int i = 4; i < 12; i++)
				Cells.Add(new Cell(i, 1));
			for (int i = 0; i < 14; i++)
				Cells.Add(new Cell(i, 2));
			for (int i = 2; i < 12; i++)
				Cells.Add(new Cell(i, 3));
			foreach (var cell in Cells)
			{
				foreach (var otherCell in Cells)
				{
					var dx = otherCell.Coordinates.X - cell.Coordinates.X;
					var dy = otherCell.Coordinates.Y - cell.Coordinates.Y;
					if (dx == 1 && dy == 0)
						cell.Neighbors.Right = otherCell;
					if (dx == -1 && dy == 0)
						cell.Neighbors.Left = otherCell;
					if (dx == 0 && dy == 1)
						cell.Neighbors.Down = otherCell;
					if (dx == 0 && dy == -1)
						cell.Neighbors.Up = otherCell;
				}
			}
		}

		private void GenerateCrew()
		{
			Crew = new List<CrewMember>
			{
				new CrewMember(Cells[1], Alignment),
				new CrewMember(Cells[7], Alignment),
				new CrewMember(Cells[10], Alignment),
				new CrewMember(Cells[23], Alignment)
			};
		}

		private void GenerateRooms()
		{
			Rooms = new List<Room>();
			var length = Cells.Count;
			for (int i = 0; i < length; i += 2)
			{
				if (i == 16)
				{
					Rooms.Add(new Room(new List<Cell> { Cells[i], Cells[i + 1],
					Cells[i + 2], Cells[i + 3]}));
					i += 2;
				}
				else
					Rooms.Add(new Room(new List<Cell> { Cells[i], Cells[i + 1] }));
			}
		}

		private Dictionary<RoomType, RoomStat> GenerateSpecialRoomsStat()
		{
			var roomsStat = new Dictionary<RoomType, RoomStat>
			{
				[RoomType.Radar] = new RoomStat(5, 1, 0, 0),
				[RoomType.Control] = new RoomStat(4, 2, 0, 2),
				[RoomType.Engine] = new RoomStat(4, 2, 0, 2),
				[RoomType.Generator] = new RoomStat(15, 5, 5, 0),
				[RoomType.Living] = new RoomStat(4, 2, 0, 0),
				[RoomType.Weapon] = new RoomStat(5, 1, 0, 2)
			};
			return roomsStat;
		}

		private void GenerateSpecialRooms(Dictionary<RoomType, RoomStat> RoomsStat)
		{
			SpecialRooms = new List<SpecialRoom>();
			SpecialRooms.Add(new SpecialRoom(Rooms[0], RoomType.Radar, RoomsStat[RoomType.Radar]));
			Rooms[0] = SpecialRooms[0];
			SpecialRooms.Add(new SpecialRoom(Rooms[3], RoomType.Control, RoomsStat[RoomType.Control]));
			Rooms[3] = SpecialRooms[1];
			SpecialRooms.Add(new SpecialRoom(Rooms[5], RoomType.Engine, RoomsStat[RoomType.Engine]));
			Rooms[5] = SpecialRooms[2];
			SpecialRooms.Add(new SpecialRoom(Rooms[7], RoomType.Generator, RoomsStat[RoomType.Generator]));
			Rooms[7] = SpecialRooms[3];
			SpecialRooms.Add(new SpecialRoom(Rooms[8], RoomType.Living, RoomsStat[RoomType.Living]));
			Rooms[8] = SpecialRooms[4];
			SpecialRooms.Add(new SpecialRoom(Rooms[10], RoomType.Weapon, RoomsStat[RoomType.Weapon]));
			Rooms[10] = SpecialRooms[5];
		}
	}
}
