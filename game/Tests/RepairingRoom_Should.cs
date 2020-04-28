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

            ship.Rooms[0].CurrentDurability = 10;
            ship.Stats.HP = 10;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(12, ship.SpecialRooms[0].CurrentDurability);
            Assert.AreEqual(10, ship.Stats.HP);
        }

        [Test]
        public void TestRepairFullRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(100, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(100, ship.SpecialRooms[0].CurrentDurability);
        }

        [Test]
        public void TestRepairMovingCrewMemberRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            ship.Rooms[0].CurrentDurability = 10;

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(12, ship.SpecialRooms[0].CurrentDurability);

            InterfaceCommands.MoveCrewMember(ship.Crew[0], ship.Cells[5], ship);
            Assert.AreEqual(12, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(12, ship.SpecialRooms[0].CurrentDurability);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(13, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(13, ship.SpecialRooms[0].CurrentDurability);

            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(14, ship.Rooms[0].CurrentDurability);
            Assert.AreEqual(14, ship.SpecialRooms[0].CurrentDurability);
        }
    }
}
