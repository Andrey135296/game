using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	static class CrewActionsHandler
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
							if (specialRoom.Stat.EmptyWorkingSpaces > 0)
							{
								crewMember.Action = CrewAction.Working;
								specialRoom.Stat.EmptyWorkingSpaces--;
							}
						}
						break;
					case CrewAction.Working:
						if (specialRoom.CurrentDurability < specialRoom.MaxDurability)
							specialRoom.CurrentDurability+=crewMember.RepairSpeed;
						specialRoom.CurrentDurability = Math.Min(specialRoom.CurrentDurability, specialRoom.MaxDurability);
						break;
					default:
						throw new NotImplementedException("unknown crew action");
				}
			}
		}

		public static void MoveCrewMember(Ship ship, CrewMember crewMember)
		{
			var room = ship.Rooms.Where(r => r.CrewMembers.Contains(crewMember)).FirstOrDefault();
			if (room.Type == RoomType.Living)
				crewMember.CurrentHP = Math.Min(crewMember.CurrentHP + ship.Stats.Heal, crewMember.MaxHP);
			if (crewMember.Cell == crewMember.Destination)
			{
				crewMember.Action = CrewAction.Idle;
				crewMember.Cell.stationed = crewMember;
				crewMember.Destination = null;
				return;
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
