﻿using System;
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
        //public Cell[,] Cells;
        public List<Cell> Cells;
		public ShipStat Stats;
        public List<Weapon> Weapons;
	}

	class Titan : Ship
	{
		public Titan()
		{
			Stats = new ShipStat(10, 20);
            GenerateSpecialRoomsStat();
			GenerateCells();
            GenerateRooms();
            GenerateSpecialRooms();
            GenerateCrew();
            GenerateWeapons();
		}

        private void GenerateWeapons()
        {
            Weapons.Add(new LaserM0());
            Weapons.Add(new LaserM0());
        }

        private void GenerateCells()
        {
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
            Crew.Add(new CrewMember(Cells[1]));
            Crew.Add(new CrewMember(Cells[7]));
            Crew.Add(new CrewMember(Cells[10]));
            Crew.Add(new CrewMember(Cells[23])); 
        }



        private void GenerateRooms()
        {
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

        private void GenerateSpecialRoomsStat()
        {
            Stats.EnergyConsumption[RoomType.Radar] = new EnergyStat(5, 1, 0);
            Stats.EnergyConsumption[RoomType.Control] = new EnergyStat(4, 2, 0);
            Stats.EnergyConsumption[RoomType.Engine] = new EnergyStat(4, 2, 0);
            Stats.EnergyConsumption[RoomType.Generator] = new EnergyStat(15, 5, 5);
            Stats.EnergyConsumption[RoomType.Living] = new EnergyStat(4, 2, 0);
            Stats.EnergyConsumption[RoomType.Weapon] = new EnergyStat(5, 1, 0);
        }

        private void GenerateSpecialRooms()
        {
            SpecialRooms.Add(new SpecialRoom(Rooms[0], RoomType.Radar, Stats.EnergyConsumption[RoomType.Radar]));
            SpecialRooms.Add(new SpecialRoom(Rooms[3], RoomType.Control, Stats.EnergyConsumption[RoomType.Control]));
            SpecialRooms.Add(new SpecialRoom(Rooms[5], RoomType.Engine, Stats.EnergyConsumption[RoomType.Engine]));
            SpecialRooms.Add(new SpecialRoom(Rooms[7], RoomType.Generator, Stats.EnergyConsumption[RoomType.Generator]));
            SpecialRooms.Add(new SpecialRoom(Rooms[8], RoomType.Living, Stats.EnergyConsumption[RoomType.Living]));
            SpecialRooms.Add(new SpecialRoom(Rooms[10], RoomType.Weapon, Stats.EnergyConsumption[RoomType.Weapon]));
        }

        //private void GenerateRooms()
        //{
        //    for (int i = 6; i < 8; i += 2)
        //        Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i, 0], Cells[i + 1, 0] }));
        //    for (int i = 4; i < 12; i += 2)
        //        Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i, 1], Cells[i + 1, 1] }));
        //    for (int i = 0; i < 14; i += 2)
        //    {
        //        if (i == 8)
        //        {
        //            Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i, 2], Cells[i + 1, 2],
        //            Cells[i + 2, 2], Cells[i + 3, 2]}));
        //            i += 2;
        //        }
        //        else
        //            Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i, 2], Cells[i + 1, 2] }));
        //    }
        //    for (int i = 2; i < 12; i += 2)
        //        Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i, 3], Cells[i + 1, 3] }));
        //}

        //private void GenerateCells()
        //{
        //    Cells = new Cell[14, 4];
        //    for (int i = 6; i < 8; i++)
        //        Cells[i, 0] = new Cell(i, 0);
        //    for (int i = 4; i < 12; i++)
        //        Cells[i, 1] = new Cell(i, 1);
        //    for (int i = 0; i < 14; i++)
        //        Cells[i, 2] = new Cell(i, 2);
        //    for (int i = 2; i < 12; i++)
        //        Cells[i, 3] = new Cell(i, 3);
        //    foreach (var cell in Cells)
        //    {
        //        foreach (var otherCell in Cells)
        //        {
        //            var dx = otherCell.coordinates.X - cell.coordinates.X;
        //            var dy = otherCell.coordinates.Y - cell.coordinates.Y;
        //            if (dx == 1 && dy == 0)
        //                cell.neighbors.right = otherCell;
        //            if (dx == -1 && dy == 0)
        //                cell.neighbors.left = otherCell;
        //            if (dx == 0 && dy == 1)
        //                cell.neighbors.down = otherCell;
        //            if (dx == 0 && dy == -1)
        //                cell.neighbors.up = otherCell;
        //        }
        //    }
        //}

    }
}