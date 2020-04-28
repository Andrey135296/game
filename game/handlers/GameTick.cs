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
			if (gameModel.Ship1.Alignment != Alignment.Player)
				throw new Exception("You are a looooooser!!!!!!");
			CrewActionsHandler.TickCrew(gameModel.Ship1);
			CrewActionsHandler.TickCrew(gameModel.Ship2);
			if (gameModel.Ship2!=null && gameModel.Ship2.Alignment == Alignment.Enemy)
			{
				WeaponsHandler.Tick(gameModel.Ship1, gameModel.Ship2, gameModel.TickLength);
				WeaponsHandler.Tick(gameModel.Ship2, gameModel.Ship1, gameModel.TickLength);
			}
			SpecialRoomBonusCalculator.Recalculate(gameModel.Ship1);
			SpecialRoomBonusCalculator.Recalculate(gameModel.Ship2);
		}
	}
}
