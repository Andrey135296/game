using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace game
{
    [TestFixture]
    public class RepairingRooms_Shold
    {
        [Test]
        public void TestRepairRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            //InterfaceCommands.MoveCrewMember(ship.Crew[1], ship.Cells[5], ship);
            //CrewActionsHandler.TickCrew(ship);
            //Assert.AreEqual(ship.Cells[5], ship.Crew[1].Cell);

            InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 10;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Crew[1].CurrentHP);
        }
    }
}
