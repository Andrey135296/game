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

        [Test]
        public void testShipCreation()
        {
            var ship = new TestShip();
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Working));
            ship.Crew[2].Destination = ship.Cells[2];
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual( ship.Cells[2], ship.Crew[2].Cell);
        }
    }
}
