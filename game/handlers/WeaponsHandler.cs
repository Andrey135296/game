using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public class WeaponsHandler
	{
		public static void Tick(Ship attackingShip, Ship attackedShip, int time)
		{
			if (attackingShip == null || attackedShip == null)
				return;
			Reload(attackingShip.Weapons, time);
			Fire(attackingShip.Weapons, attackedShip);
		}

		private static void Reload(List<Weapon> weapons, int time)
		{
			foreach (var weapon in weapons.Where(w => w.IsOnline && w.TimeLeftToCoolDown > 0))
			{
				weapon.TimeLeftToCoolDown -= time;
			}
		}

		private static void Fire(List<Weapon> weapons, Ship attackedShip)
		{
			foreach (var weapon in weapons.Where(w => w.IsOnline && w.TimeLeftToCoolDown <= 0 && w.Target!=null))
			{
				attackedShip.Stats.HP -= weapon.Damage;
				weapon.TimeLeftToCoolDown += weapon.CoolDownTime;
				if (attackedShip.Stats.HP <= 0)
					attackedShip.Alignment = Alignment.Wrekage;
				if (attackedShip.SpecialRooms.Contains(weapon.Target))
				{
					var specialRoom = (SpecialRoom)weapon.Target;
					specialRoom.CurrentDurability = Math.Max(0, specialRoom.CurrentDurability - weapon.Damage);
				}
				foreach (var c in attackedShip.Crew)
					if (weapon.Target.Cells.Contains(c.Cell))
					{
						c.CurrentHP -= weapon.Damage;
						if (c.CurrentHP <= 0)
							c.IsAlive = false;
					}
			}
		}
	}
}
