using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	public static class GameTick
	{
		public static void Tick(GameModel gameModel)
		{
			if (gameModel.IsRunning)
			{
				if (gameModel.PlayerShip.Alignment != Alignment.Player)
					throw new Exception("You are a looooooser!!!!!!");
				CrewActionsHandler.TickCrew(gameModel.PlayerShip);
				CrewActionsHandler.TickCrew(gameModel.OtherShip);
				if (gameModel.OtherShip != null && gameModel.OtherShip.Alignment == Alignment.Enemy)
				{
					WeaponsHandler.Tick(gameModel.PlayerShip, gameModel.OtherShip, gameModel.TickLength);
					WeaponsHandler.Tick(gameModel.OtherShip, gameModel.PlayerShip, gameModel.TickLength);
				}
				SpecialRoomBonusCalculator.Recalculate(gameModel.PlayerShip);
				SpecialRoomBonusCalculator.Recalculate(gameModel.OtherShip);
			}
		}
	}
}
