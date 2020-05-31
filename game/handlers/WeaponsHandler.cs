using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class WeaponsHandler
	{
		private readonly static Random random = new Random();

		public static void Tick(Ship attackingShip, Ship attackedShip, int time)
		{
			if (attackingShip == null || attackedShip == null)
				return;
			Reload(attackingShip.Weapons, time);
			Fire(attackingShip.Weapons, attackedShip, attackingShip.Stats.DamageMultiplier);
		}

		private static void Reload(List<Weapon> weapons, int time)
		{
			foreach (var weapon in weapons.Where(w => w.IsOnline && w.TimeLeftToCoolDown > 0))
			{
				weapon.TimeLeftToCoolDown -= time;
			}
		}

		private static void Fire(List<Weapon> weapons, Ship attackedShip, double damageMultiplier)
		{
			foreach (var weapon in weapons.Where(w => w.IsOnline && w.TimeLeftToCoolDown <= 0 && w.Target!=null))
			{
				if (random.Next(0, 100) < attackedShip.Stats.Evasion)
				{
					weapon.TimeLeftToCoolDown += weapon.CoolDownTime;
					continue;
				}
				attackedShip.Stats.CurrentHP -= (int)(weapon.Damage*damageMultiplier);
				attackedShip.Stats.CurrentHP = Math.Max(0, attackedShip.Stats.CurrentHP);
				weapon.TimeLeftToCoolDown += weapon.CoolDownTime;
				if (attackedShip.Stats.CurrentHP == 0)
					attackedShip.Alignment = Alignment.Wrekage;
					
				//if (attackedShip.SpecialRooms.Contains(weapon.Target))
				//{
				//	var specialRoom = (SpecialRoom)weapon.Target;
				//	specialRoom.CurrentDurability = 
				//		Math.Max(0, specialRoom.CurrentDurability - (int)(weapon.Damage * damageMultiplier));
				//}
				var room = weapon.Target;
				room.CurrentDurability =
					Math.Max(0, room.CurrentDurability - (int)(weapon.Damage * damageMultiplier));

				foreach (var crewMember in attackedShip.Crew)
					if (weapon.Target.Cells.Contains(crewMember.Cell))
					{
						crewMember.CurrentHP -= (int)(weapon.Damage * damageMultiplier);
						crewMember.CurrentHP = Math.Max(0, crewMember.CurrentHP);
						if (crewMember.CurrentHP == 0)
							crewMember.IsAlive = false;
					}
			}
		}
	}
}