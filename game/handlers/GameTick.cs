using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
	class GameTick
	{
		public static void Tick(GameModel gameModel)
		{
			if (gameModel.ship1.alignment != Alignment.Player)
				throw new Exception("You are a looooooser!!!!!!");
			CrewActionsHandler.TickCrew(gameModel.ship1);
			CrewActionsHandler.TickCrew(gameModel.ship2);
			if (gameModel.ship2!=null && gameModel.ship2.alignment == Alignment.Enemy)
			{
				WeaponsHandler.Tick(gameModel.ship1, gameModel.ship2, gameModel.tickLength);
				WeaponsHandler.Tick(gameModel.ship2, gameModel.ship1, gameModel.tickLength);
			}
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship1);
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship2);
		}
	}
}
