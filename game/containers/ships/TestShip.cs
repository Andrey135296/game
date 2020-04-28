using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class TestShip : Ship
	{
		public TestShip(Alignment alignment)
		{
			this.Alignment = alignment;
			Stats = new ShipStat(200);
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
					new LaserM1(),
					new LaserM2(),
					new LaserM0(),
				};
		}

		private void GenerateCells()
		{
			Cells = new List<Cell>
				{
					new Cell(0, 0),
					new Cell(1, 0),
					new Cell(2, 0),
					new Cell(3, 0),
					new Cell(4, 0),
					new Cell(1, 1),
					new Cell(1, 2),
					new Cell(1, 3),
					new Cell(2, 3),
					new Cell(3, 3),
					new Cell(3, 2),
					new Cell(3, 1),
					new Cell(5, 0),
					new Cell(6, 0)
				};

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
				new CrewMember(Cells[0], Alignment),
				new CrewMember(Cells[1], Alignment),
				new CrewMember(Cells[3], Alignment),
				new CrewMember(Cells[4], Alignment),
				new CrewMember(Cells[9], Alignment)
			};
		}



		private void GenerateRooms()
		{
			Rooms = new List<Room>();
			Rooms.Add(new Room(new List<Cell> { Cells[0], Cells[1], Cells[2] }));
			Rooms.Add(new Room(new List<Cell> { Cells[3], Cells[4] }));
			Rooms.Add(new Room(new List<Cell> { Cells[5], Cells[6] }));
			Rooms.Add(new Room(new List<Cell> { Cells[7], Cells[8] }));
			Rooms.Add(new Room(new List<Cell> { Cells[9], Cells[10], Cells[11] }));
			Rooms.Add(new Room(new List<Cell> { Cells[12], Cells[13] }));
		}

		private Dictionary<RoomType, RoomStat> GenerateSpecialRoomsStat()
		{
			var RoomsStat = new Dictionary<RoomType, RoomStat>();
			RoomsStat[RoomType.Radar] = new RoomStat(5, 1, 0, 0);
			RoomsStat[RoomType.Control] = new RoomStat(4, 2, 0, 2);
			RoomsStat[RoomType.Engine] = new RoomStat(4, 2, 0, 2);
			RoomsStat[RoomType.Generator] = new RoomStat(15, 5, 5, 0);
			RoomsStat[RoomType.Living] = new RoomStat(4, 2, 0, 0);
			RoomsStat[RoomType.Weapon] = new RoomStat(5, 2, 0, 2);
			return RoomsStat;
		}

		private void GenerateSpecialRooms(Dictionary<RoomType, RoomStat> RoomsStat)
		{
			SpecialRooms = new List<SpecialRoom>();
			SpecialRooms.Add(new SpecialRoom(Rooms[0], RoomType.Control, RoomsStat[RoomType.Control]));
			Rooms[0] = SpecialRooms[0];
			SpecialRooms.Add(new SpecialRoom(Rooms[1], RoomType.Radar, RoomsStat[RoomType.Radar]));
			Rooms[1] = SpecialRooms[1];
			SpecialRooms.Add(new SpecialRoom(Rooms[2], RoomType.Living, RoomsStat[RoomType.Living]));
			Rooms[2] = SpecialRooms[2];
			SpecialRooms.Add(new SpecialRoom(Rooms[3], RoomType.Generator, RoomsStat[RoomType.Generator]));
			Rooms[3] = SpecialRooms[3];
			SpecialRooms.Add(new SpecialRoom(Rooms[4], RoomType.Engine, RoomsStat[RoomType.Engine]));
			Rooms[4] = SpecialRooms[4];
			SpecialRooms.Add(new SpecialRoom(Rooms[5], RoomType.Weapon, RoomsStat[RoomType.Weapon]));
			Rooms[5] = SpecialRooms[5];
		}
	}
}
