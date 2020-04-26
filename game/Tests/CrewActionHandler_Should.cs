using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace game.Tests
{
	[TestFixture]
	public class CrewActionHandler_Should
	{
		[Test]
		public void startsWorking()
		{
			var ship = new Titan();
			Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
			CrewActionsHandler.TickCrew(ship);
			Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Working));
		}
	}
}
