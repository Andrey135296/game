using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Room
    {
        public List<Cell> Cells;
        public List<CrewMember> CrewMembers;
        public int CurrentDurability;
        public int MaxDurability;
        public bool IsIntact;
        public RoomType Type;
        public Room(List<Cell> cells)
        {
            Cells = cells;
            CrewMembers = cells.Select(c => c.stationed).Where(c => c != null).ToList();
            MaxDurability = 100;
            CurrentDurability = MaxDurability;
            IsIntact = true;
        }
    }

    class SpecialRoom : Room
    {
        //public bool IsOnline;
        public EnergyStat RoomEnergyStat;
        public int EmptyWorkingSpaces;
        public int WorkingSpaces = 2;
        //public List<CrewMember> WorkingCrewMembers;

        public SpecialRoom(List<Cell> cells, RoomType roomType, EnergyStat roomEnergyStat) : base(cells)
        {
            //IsOnline = false;
            Type = roomType;
            RoomEnergyStat = roomEnergyStat;
			EmptyWorkingSpaces = WorkingSpaces;
        }
        public SpecialRoom(Room room, RoomType roomType, EnergyStat roomEnergyStat) : base(room.Cells)
        {
            Type = roomType;
            RoomEnergyStat = roomEnergyStat;
			EmptyWorkingSpaces = WorkingSpaces;
		}
    }

    //class EmptyRoom : Room
    //{
    //    public EmptyRoom(List<Cell> cells)
    //    {
    //        Type = RoomType.Empty;
    //        Cells = cells;
    //        CrewMembers = cells.Select(c => c.stationed).Where(c => c != null).ToList();
    //        Durability = 100;
    //        CurrentDurability = Durability;
    //        IsIntact = true;
    //    }
    //}

    //class GeneratorRoom : SpecialRoom
    //{
    //    public GeneratorRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Generator;
    //    }

    //}

    //class RadarRoom : SpecialRoom
    //{
    //    public RadarRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Radar;
    //    }
    //}

    //class WeaponRoom : SpecialRoom
    //{
    //    public WeaponRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Weapon;
    //    }
    //}

    //class ControlRoom : SpecialRoom
    //{

    //    public ControlRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Control;
    //    }

    //}

    //class LivingRoom : SpecialRoom
    //{
    //    public LivingRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Living;
    //    }
    //}

    //class EngineRoom : SpecialRoom
    //{
    //    public EngineRoom(EmptyRoom emptyRoom, EnergyStat roomEnergyStat) : base(emptyRoom, roomEnergyStat)
    //    {
    //        Type = RoomType.Engine;
    //    }
    //}
}