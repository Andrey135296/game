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
			CrewActionsHandler.TickCrew(gameModel.ship1);
			CrewActionsHandler.TickCrew(gameModel.ship2);
			WeaponsHandler.Tick(gameModel.ship1, gameModel.ship2, gameModel.tickLength);
			WeaponsHandler.Tick(gameModel.ship2, gameModel.ship1, gameModel.tickLength);
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship1);
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship2);
		}
	}
}
