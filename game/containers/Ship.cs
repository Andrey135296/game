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
    }

    class Titan : Ship
    {
        public Titan()
        {
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
                new CrewMember(Cells[1]),
                new CrewMember(Cells[7]),
                new CrewMember(Cells[10]),
                new CrewMember(Cells[23])
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

        private Dictionary<RoomType, EnergyStat> GenerateSpecialRoomsStat()
        {
			var EnergyConsumption = new Dictionary<RoomType, EnergyStat>();
            EnergyConsumption[RoomType.Radar] = new EnergyStat(5, 1, 0);
            EnergyConsumption[RoomType.Control] = new EnergyStat(4, 2, 0);
            EnergyConsumption[RoomType.Engine] = new EnergyStat(4, 2, 0);
            EnergyConsumption[RoomType.Generator] = new EnergyStat(15, 5, 5);
            EnergyConsumption[RoomType.Living] = new EnergyStat(4, 2, 0);
            EnergyConsumption[RoomType.Weapon] = new EnergyStat(5, 1, 0);
			return EnergyConsumption;
        }

        private void GenerateSpecialRooms(Dictionary<RoomType, EnergyStat> EnergyConsumption)
        {
			SpecialRooms = new List<SpecialRoom>();
			SpecialRooms.Add(new SpecialRoom(Rooms[0], RoomType.Radar, EnergyConsumption[RoomType.Radar]));
			Rooms[0] = SpecialRooms[0];
			SpecialRooms.Add(new SpecialRoom(Rooms[3], RoomType.Control, EnergyConsumption[RoomType.Control]));
			Rooms[3] = SpecialRooms[1];
			SpecialRooms.Add(new SpecialRoom(Rooms[5], RoomType.Engine, EnergyConsumption[RoomType.Engine]));
			Rooms[5] = SpecialRooms[2];
			SpecialRooms.Add(new SpecialRoom(Rooms[7], RoomType.Generator, EnergyConsumption[RoomType.Generator]));
			Rooms[7] = SpecialRooms[3];
			SpecialRooms.Add(new SpecialRoom(Rooms[8], RoomType.Living, EnergyConsumption[RoomType.Living]));
			Rooms[8] = SpecialRooms[4];
			SpecialRooms.Add(new SpecialRoom(Rooms[10], RoomType.Weapon, EnergyConsumption[RoomType.Weapon]));
			Rooms[10] = SpecialRooms[5];
		}
    }


    class TestShip : Ship
    {
        public TestShip()
        {
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
                    new Cell(4, 0)
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
                new CrewMember(Cells[0]),
                new CrewMember(Cells[1]),
                new CrewMember(Cells[3]),
                new CrewMember(Cells[4])
            };
        }



        private void GenerateRooms()
        {
            Rooms = new List<Room>();
            Rooms.Add(new Room(new List<Cell> { Cells[0], Cells[1], Cells[2] }));
            Rooms.Add(new Room(new List<Cell> { Cells[3], Cells[4] }));
        }

		private Dictionary<RoomType, EnergyStat> GenerateSpecialRoomsStat()
		{
			var EnergyConsumption = new Dictionary<RoomType, EnergyStat>();
			EnergyConsumption[RoomType.Radar] = new EnergyStat(5, 1, 0);
			EnergyConsumption[RoomType.Control] = new EnergyStat(4, 2, 0);
			EnergyConsumption[RoomType.Engine] = new EnergyStat(4, 2, 0);
			EnergyConsumption[RoomType.Generator] = new EnergyStat(15, 5, 5);
			EnergyConsumption[RoomType.Living] = new EnergyStat(4, 2, 0);
			EnergyConsumption[RoomType.Weapon] = new EnergyStat(5, 1, 0);
			return EnergyConsumption;
		}

		private void GenerateSpecialRooms(Dictionary<RoomType, EnergyStat> EnergyConsumption)
		{
			SpecialRooms = new List<SpecialRoom>();
			SpecialRooms.Add(new SpecialRoom(Rooms[0], RoomType.Radar, EnergyConsumption[RoomType.Radar]));
			Rooms[0] = SpecialRooms[0];
			SpecialRooms.Add(new SpecialRoom(Rooms[1], RoomType.Control, EnergyConsumption[RoomType.Control]));
			Rooms[1] = SpecialRooms[1];
		}
    }
}