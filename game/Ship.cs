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
        //public Cell[,] Cells;
        public List<Cell> Cells;
		public ShipStat Stats;
	}

	class Titan : Ship
	{
		public Titan()
		{
			Stats = new ShipStat(10, 20);
			GenerateCells();
            GenerateRooms();

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

        private void GenerateRooms()
        {
            var length = Cells.Count;
            for (int i = 0; i < length; i += 2)
            {
                if (i == 16)
                {
                    Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i], Cells[i + 1],
                    Cells[i + 2], Cells[i + 3]}));
                    i += 2;
                }
                else
                    Rooms.Add(new EmptyRoom(new List<Cell> { Cells[i], Cells[i + 1] }));
            }
        }
        
        private void GenerateSpecialRooms()
        {
            Rooms[0] = new SpecialRoom(Rooms[0], RoomType.Radar, Stats.EnergyConsumption[RoomType.Radar] );
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