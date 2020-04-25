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
        public int Durability;
        public bool IsIntact;
        public RoomType Type;
	}

    class SpecialRoom : Room
    {
        //public bool IsOnline;
        public int EnergyLimit;
        public int CurrentEnergyLimit;
        public int CurrentEnergy;
        public int EmptyWorkingSpaces;
        public int WorkingSpaces = 2;
        //public List<CrewMember> WorkingCrewMembers;

        public SpecialRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit)
        {
            Cells = emptyRoom.Cells;
            CrewMembers = emptyRoom.CrewMembers;
            Durability = 100;
            CurrentDurability = Durability;
            IsIntact = true;
            //IsOnline = false;
            EnergyLimit = energyLimit;
            CurrentEnergyLimit = currentEnergyLimit;
            CurrentEnergy = 0;
        }
    }

    class EmptyRoom : Room
    {
        public EmptyRoom(List<Cell> cells)
        {
            Type = RoomType.Empty;
            Cells = cells;
            CrewMembers = cells.Select(c => c.stationed).Where(c => c != null).ToList();
            Durability = 100;
            CurrentDurability = Durability;
            IsIntact = true;
        }
    }

    class GeneratorRoom : SpecialRoom
    {
        public GeneratorRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Generator;
            CurrentEnergy = CurrentEnergyLimit;
        }

    }

    class RadarRoom : SpecialRoom
    {
        public RadarRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Radar;
        }
    }

    class WeaponRoom : SpecialRoom
    {
        public WeaponRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Weapon;
        }
    }

    class ControlRoom : SpecialRoom
    {

        public ControlRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Control;
        }

    }

    class LivingRoom : SpecialRoom
    {
        public LivingRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Living;
        }
    }

    class EngineRoom : SpecialRoom
    {
        public EngineRoom(EmptyRoom emptyRoom, int energyLimit, int currentEnergyLimit) : base(emptyRoom, energyLimit, currentEnergyLimit)
        {
            Type = RoomType.Engine;
        }
    }
}