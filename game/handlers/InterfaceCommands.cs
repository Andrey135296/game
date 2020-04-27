using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.handlers
{
	static class InterfaceCommands
	{
		public static void MoveCrewMember(CrewMember crewMember, Cell cell, Ship ship)
		{
			if (crewMember.alignment == Alignment.Player)
				if (!ship.Crew.Any(c => c.Destination == cell) && cell.stationed == null)
				{
					crewMember.Destination = cell;
					crewMember.Action = CrewAction.Moving;
				}
		}

		public static void TrySetRoomEnergyAllocation(Room room, int energy, Ship ship)
		{
			throw new NotImplementedException();
		}
	}
}
