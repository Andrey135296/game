using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class CrewActionsHandler
	{
		public static void TickCrew(Ship ship)
		{
			foreach (var crewMember in ship.Crew.Where(cm => cm.IsAlive))
			{
				var specialRoom = ship.SpecialRooms.Where(r => r.CrewMembers.Contains(crewMember)).FirstOrDefault();
				switch (crewMember.Action)
				{
					case CrewAction.Moving:
						MoveCrewMember(ship, crewMember);
						break;
					case CrewAction.Idle:
						if (specialRoom != null)
						{
							if (specialRoom.EmptyWorkingSpaces > 0)
							{
								crewMember.Action = CrewAction.Working;
								specialRoom.EmptyWorkingSpaces--;
							}
						}
						break;
					case CrewAction.Working:
						if (specialRoom.CurrentDurability < specialRoom.Durability)
							specialRoom.CurrentDurability+=crewMember.RepairSpeed;
						specialRoom.CurrentDurability = Math.Min(specialRoom.CurrentDurability, specialRoom.Durability);
						break;
					default:
						throw new NotImplementedException("unknown crew action");
				}
			}
		}

		public static void MoveCrewMember(Ship ship, CrewMember crewMember)
		{
			var room = ship.Rooms.Where(r => r.CrewMembers.Contains(crewMember)).FirstOrDefault();
			//var map = ship.Cells;
			if (crewMember.Cell == crewMember.Destination)
			{
				crewMember.Action = CrewAction.Idle;
				crewMember.Cell.stationed = crewMember;
			}
			crewMember.Cell = BFS(crewMember).Last();
			if (!room.Cells.Contains(crewMember.Cell))
			{
				room.CrewMembers.Remove(crewMember);
				ship.Rooms.Where(r => r.Cells.Contains(crewMember.Cell)).FirstOrDefault().CrewMembers.Add(crewMember);
			}
		}

		private static List<Cell> BFS(CrewMember crewMember)
		{
			var end = crewMember.Destination;
			var queue = new Queue<Cell>();
			queue.Enqueue(crewMember.Cell);
			var dictionary = new Dictionary<Cell, int>();
			dictionary[crewMember.Cell] = 0;
			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				foreach (var n in node.neighbors)
				{
					if (dictionary.ContainsKey(n))
						continue;
					dictionary[n] = dictionary[node] + 1;
					queue.Enqueue(n);
				}
			}
			if (!dictionary.ContainsKey(end))
				throw new Exception("wtf");
			var curNode = end;
			var path = new List<Cell>();
			while (curNode != crewMember.Cell)
			{
				path.Add(curNode);
				var min = curNode.neighbors.Min(n => dictionary[n]);
				curNode = curNode.neighbors.Where(n => dictionary[n] == min).First();
			}
			return path;
		}
	}
}
