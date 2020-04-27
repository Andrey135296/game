using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace game
{
	[TestFixture]
	public class CrewActionHandler_Should
	{
        [Test]
        public void TestStartWorkingAndNotWorking()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[0].Action);
            Assert.AreEqual(CrewAction.Working, ship.Crew[1].Action);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[3].Action);
            Assert.AreEqual(CrewAction.Working, ship.Crew[4].Action);
        }

        [Test]
        public void TestMove()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
        }

        [Test]
        public void TestCrewStatesAfterMoving()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Destination);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(null, ship.Crew[2].Destination);
        }


        [Test]
        public void TestCrewActionsAfterMovingToFullRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
        }

        [Test]
        public void TestCrewActionsAfterMovingToEmptyRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[2].Action);
        }

        [Test]
        public void TestRoomStatesAfterMovingToNotFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(1, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestRoomStatesAfterMovingToFullWorkingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[11], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(1, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
            InterfaceCommands.MoveCrewMember(ship.Crew[3], ship.Cells[10], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(0, ship.SpecialRooms[3].Stat.EmptyWorkingSpaces);
        }

        [Test]
        public void TestWayToDistination()
        {
            var ship = new TestShip(Alignment.Player);
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[4], ship.Cells[2], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[10], ship.Crew[4].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[11], ship.Crew[4].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[3], ship.Crew[4].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
        }

        public void TestTwoMovingCrewMembers()
        {
            var ship = new TestShip(Alignment.Player);
            CrewActionsHandler.TickCrew(ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[4], ship.Cells[2], ship);
            InterfaceCommands.MoveCrewMember(ship.Crew[2], ship.Cells[8], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[10], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[11], ship.Crew[2].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[11], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[10], ship.Crew[2].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[3], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[9], ship.Crew[2].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[8], ship.Crew[2].Cell);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[4].Cell);
            Assert.AreEqual(ship.Cells[8], ship.Crew[2].Cell);
        }

    }
}
