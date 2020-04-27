using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.handlers;
using NUnit.Framework;

namespace game.Tests
{
	[TestFixture]
	public class CrewActionHandler_Should
	{
        [Test]
        public void StartWorkingAndNotWorking()
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

        //[Test]
        //public void CrewStartsWorking()
        //{
        //    var ship = new Titan(Alignment.Player);
        //    Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
        //    CrewActionsHandler.TickCrew(ship);
        //    Assert.AreEqual(CrewAction.Idle, ship.Crew[0].Action);
        //    for (int i = 1; i < ship.Crew.Count; i++)
        //        Assert.AreEqual(CrewAction.Working, ship.Crew[i].Action);
        //}

        [Test]
        public void CanMove()
        {
            var ship = new TestShip(Alignment.Player);
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            ship.Crew[2].Destination = ship.Cells[2];
            ship.Crew[2].Action = CrewAction.Moving;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
        }

        [Test]
        public void CrewCorrectStatesAfterMoving()
        {
            var ship = new TestShip(Alignment.Player);
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            ship.Crew[2].Destination = ship.Cells[2];
            ship.Crew[2].Action = CrewAction.Moving;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Destination);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[2], ship.Crew[2].Cell);
            Assert.AreEqual(null, ship.Crew[2].Destination);
        }


        [Test]
        public void CrewCorrectActionsAfterMovingToFullRoom()
        {
            var ship = new TestShip(Alignment.Player);
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            ship.Crew[2].Destination = ship.Cells[2];
			ship.Crew[2].Action = CrewAction.Moving;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
        }

        [Test]
        public void CrewCorrectActionsAfterMovingToEmptyRoom()
        {
            var ship = new TestShip(Alignment.Player);
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            ship.Crew[2].Destination = ship.Cells[11];
            ship.Crew[2].Action = CrewAction.Moving;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[2].Action);
        }

        [Test]
        public void RoomCorrectStatesAfterMovingFrom()
        {
            var ship = new TestShip(Alignment.Player);
            //Assert.AreEqual
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);
            ship.Crew[2].Destination = ship.Cells[11];
            ship.Crew[2].Action = CrewAction.Moving;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Moving, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Idle, ship.Crew[2].Action);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(CrewAction.Working, ship.Crew[2].Action);
        }

    }
}
