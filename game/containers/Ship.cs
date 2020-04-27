using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    abstract class Ship
    {
        public List<CrewMember> Crew;
        public List<Room> Rooms;
        public List<SpecialRoom> SpecialRooms;
        public List<Cell> Cells;
        public ShipStat Stats;
        public List<Weapon> Weapons;
		public Alignment alignment;
    }

    class Titan : Ship
    {
        public Titan(Alignment alignment)
        {
			this.alignment = alignment;
            Stats = new ShipStat(2000);
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
                    var dx = otherCell.coordinates.X - cell.coordinates.X;
                    var dy = otherCell.coordinates.Y - cell.coordinates.Y;
                    if (dx == 1 && dy == 0)
                        cell.neighbors.right = otherCell;
                    if (dx == -1 && dy == 0)
                        cell.neighbors.left = otherCell;
                    if (dx == 0 && dy == 1)
                        cell.neighbors.down = otherCell;
                    if (dx == 0 && dy == -1)
                        cell.neighbors.up = otherCell;
                }
            }
        }

        private void GenerateCrew()
        {
            Crew = new List<CrewMember>
            {
                new CrewMember(Cells[1], alignment),
                new CrewMember(Cells[7], alignment),
                new CrewMember(Cells[10], alignment),
                new CrewMember(Cells[23], alignment)
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
            var RoomsStat = new Dictionary<RoomType, RoomStat>
            {
                [RoomType.Radar] = new RoomStat(5, 1, 0, 0),
                [RoomType.Control] = new RoomStat(4, 2, 0, 2),
                [RoomType.Engine] = new RoomStat(4, 2, 0, 2),
                [RoomType.Generator] = new RoomStat(15, 5, 5, 0),
                [RoomType.Living] = new RoomStat(4, 2, 0, 0),
                [RoomType.Weapon] = new RoomStat(5, 1, 0, 2)
            };
            return RoomsStat;
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


    class TestShip : Ship
    {
        public TestShip(Alignment alignment)
        {
			this.alignment = alignment;
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
                    new LaserM0()
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
                };

            foreach (var cell in Cells)
            {
                foreach (var otherCell in Cells)
                {
                    var dx = otherCell.coordinates.X - cell.coordinates.X;
                    var dy = otherCell.coordinates.Y - cell.coordinates.Y;
                    if (dx == 1 && dy == 0)
                        cell.neighbors.right = otherCell;
                    if (dx == -1 && dy == 0)
                        cell.neighbors.left = otherCell;
                    if (dx == 0 && dy == 1)
                        cell.neighbors.down = otherCell;
                    if (dx == 0 && dy == -1)
                        cell.neighbors.up = otherCell;
                }
            }
        }

        private void GenerateCrew()
        {
            Crew = new List<CrewMember>
            {
                new CrewMember(Cells[0], alignment),
                new CrewMember(Cells[1], alignment),
                new CrewMember(Cells[3], alignment),
                new CrewMember(Cells[4], alignment),
                new CrewMember(Cells[9], alignment)
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
        }

        private Dictionary<RoomType, RoomStat> GenerateSpecialRoomsStat()
        {
            var RoomsStat = new Dictionary<RoomType, RoomStat>();
            RoomsStat[RoomType.Radar] = new RoomStat(5, 1, 0, 0);
            RoomsStat[RoomType.Control] = new RoomStat(4, 2, 0, 2);
            RoomsStat[RoomType.Engine] = new RoomStat(4, 2, 0, 2);
            RoomsStat[RoomType.Generator] = new RoomStat(15, 5, 5, 0);
            RoomsStat[RoomType.Living] = new RoomStat(4, 2, 0, 0);
            RoomsStat[RoomType.Weapon] = new RoomStat(5, 1, 0, 2);
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
        }
    }
}