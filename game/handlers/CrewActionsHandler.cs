using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public static class CrewActionsHandler
	{
		public static void TickCrew(Ship ship)
		{
			if (ship == null)
				return;
			ship.Crew = ship.Crew.Where(c => c.IsAlive).ToList();
			foreach (var crewMember in ship.Crew)
			{
				var specialRoom = ship.SpecialRooms.FirstOrDefault(r => r.CrewMembers.Contains(crewMember));
				if (specialRoom != null && specialRoom.Type == RoomType.Living)
						crewMember.CurrentHP = Math.Min(crewMember.CurrentHP + ship.Stats.Heal, crewMember.MaxHP);
				switch (crewMember.Action)
				{
					case CrewAction.Moving:
						CrewMemberStep(ship, crewMember);
						break;
					case CrewAction.Idle:
						if (specialRoom != null)
						{
							if (specialRoom.CurrentDurability < specialRoom.MaxDurability)
								specialRoom.CurrentDurability += crewMember.RepairSpeed;
							specialRoom.CurrentDurability = Math.Min(specialRoom.CurrentDurability, specialRoom.MaxDurability);
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

		private static void CrewMemberStep(Ship ship, CrewMember crewMember)
		{
			var room = ship.Rooms.FirstOrDefault(r => r.CrewMembers.Contains(crewMember));
			if (crewMember.Cell == crewMember.Destination)
			{
				crewMember.Action = CrewAction.Idle;
				crewMember.Cell.Stationed = crewMember;
				crewMember.Destination = null;
				return;
			}
			crewMember.Cell = BFS(crewMember).Last();
			if (!room.Cells.Contains(crewMember.Cell))
			{
				room.CrewMembers.Remove(crewMember);
				ship.Rooms.FirstOrDefault(r => r.Cells.Contains(crewMember.Cell)).CrewMembers.Add(crewMember);
			}
		}

		private static List<Cell> BFS(CrewMember crewMember)
		{
			var end = crewMember.Destination;
			var queue = new Queue<Cell>();
			queue.Enqueue(crewMember.Cell);
			var distanceFromStart = new Dictionary<Cell, int>();
			distanceFromStart[crewMember.Cell] = 0;
			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				foreach (var neighbor in node.Neighbors)
				{
					if (distanceFromStart.ContainsKey(neighbor))
						continue;
					distanceFromStart[neighbor] = distanceFromStart[node] + 1;
					queue.Enqueue(neighbor);
				}
			}
			if (!distanceFromStart.ContainsKey(end))
				throw new Exception("cell unreachable");
			var currentNode = end;
			var path = new List<Cell>();
			while (currentNode != crewMember.Cell)
			{
				path.Add(currentNode);
				var min = currentNode.Neighbors.Min(n => distanceFromStart[n]);
				currentNode = currentNode.Neighbors.First(n => distanceFromStart[n] == min);
			}
			return path;
		}
	}
}
