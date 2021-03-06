﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public static class GameTick
	{
		public static event Action<GameModel> OnTick = StdTick;
		public static Action OnWin;
		public static Action OnLose;
		public static int LastMoneyReward = 0;
		public static int LastFuelReward = 0;
		private static Random random = new Random();
		private static void StdTick(GameModel gameModel)
		{
			if (gameModel.IsRunning)
			{
				if (gameModel.PlayerShip.Alignment != Alignment.Player)
					OnLose.Invoke();
				CrewActionsHandler.TickCrew(gameModel.PlayerShip);
				CrewActionsHandler.TickCrew(gameModel.OtherShip);
				if (gameModel.OtherShip != null && gameModel.OtherShip.Alignment == Alignment.Enemy)
				{
					WeaponsHandler.Tick(gameModel.PlayerShip, gameModel.OtherShip, gameModel.TickLength);
					WeaponsHandler.Tick(gameModel.OtherShip, gameModel.PlayerShip, gameModel.TickLength);
					if (gameModel.OtherShip.Alignment == Alignment.Wrekage)
					{
						foreach (var weapon in gameModel.PlayerShip.Weapons)
							weapon.TimeLeftToCoolDown = weapon.CoolDownTime;
						LastFuelReward = random.Next(0, 4);
						LastMoneyReward = random.Next(10, 31);
						gameModel.Fuel += LastFuelReward;
						gameModel.Money += LastMoneyReward;
						if (OnWin != null)
							OnWin.Invoke();
					}
				}
				SpecialRoomBonusCalculator.Recalculate(gameModel.PlayerShip);
				SpecialRoomBonusCalculator.Recalculate(gameModel.OtherShip);
			}
		}

		public static void Tick(GameModel gameModel)
		{
			if (gameModel.IsRunning)
				OnTick.Invoke(gameModel);
		}
	}
}
