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
				var room = ship.Rooms.Where(r => r.Cells.Contains(crewMember.Cell)).First();
				switch (crewMember.Action)
				{
					case CrewAction.Moving:
						MoveCrewMember(ship.Cells, crewMember);
						break;
					case CrewAction.Idle:
						if (room is SpecialRoom)
						{
							var specRoom = (SpecialRoom)room;
							if (specRoom.EmptyWorkingSpaces > 0)
							{
								crewMember.Action = CrewAction.Working;
								specRoom.EmptyWorkingSpaces--;
							}
						}
						break;
					case CrewAction.Working:
						if (room.CurrentDurability < room.Durability)
							room.CurrentDurability+=crewMember.RepairSpeed;
						room.CurrentDurability = Math.Min(room.CurrentDurability, room.Durability);
						break;
					default:
						throw new NotImplementedException("unknown crew action");
						break;
				}
			}
		}

		public static void MoveCrewMember(List<Cell> map, CrewMember crewMember)
		{
			if (crewMember.Cell == crewMember.Destination)
			{
				crewMember.Action = CrewAction.Idle;
				crewMember.Cell.stationed = crewMember;
			}
			crewMember.Cell = BFS(map, crewMember).Last();
		}

		private static List<Cell> BFS(List<Cell> map, CrewMember crewMember)
		{
			var end = crewMember.Destination;
			var q = new Queue<Cell>();
			q.Enqueue(crewMember.Cell);
			var d = new Dictionary<Cell, int>();
			d[crewMember.Cell] = 0;
			while (q.Count > 0)
			{
				var node = q.Dequeue();
				foreach (var n in node.neighbors)
				{
					if (d.ContainsKey(n))
						continue;
					d[n] = d[node] + 1;
					q.Enqueue(n);
				}
			}
			if (!d.ContainsKey(end))
				throw new Exception("wtf");
			var curNode = end;
			var path = new List<Cell>();
			while (curNode != crewMember.Cell)
			{
				path.Add(curNode);
				var min = curNode.neighbors.Min(n => d[n]);
				curNode = curNode.neighbors.Where(n => d[n] == min).First();
			}
			return path;
		}
	}
}
