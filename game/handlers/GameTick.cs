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
			WeaponsHandler.Tick(gameModel.ship1);
			WeaponsHandler.Tick(gameModel.ship2);
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship1);
			SpecialRoomBonusCalculator.Recalculate(gameModel.ship2);
		}
	}
}
