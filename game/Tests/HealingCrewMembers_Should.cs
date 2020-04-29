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
    public class HealingCrewMembers_Shold
    {
        [Test]
        public void TestHealOneCrewMember()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.MoveCrewMember(ship.Crew[1], ship.Cells[5], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[5], ship.Crew[1].Cell);

            PlayerCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 10;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Crew[1].CurrentHP);
        }

        [Test]
        public void TestHealOneCrewMemberInOfflineRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.MoveCrewMember(ship.Crew[1], ship.Cells[5], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[5], ship.Crew[1].Cell);

            //InterfaceCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 10;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(10, ship.Crew[1].CurrentHP);
        }

        [Test]
        public void TestOverHealCrewMember()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.MoveCrewMember(ship.Crew[1], ship.Cells[5], ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[5], ship.Crew[1].Cell);

            PlayerCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 99;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(100, ship.Crew[1].CurrentHP);
        }

        [Test]
        public void TestHealTwoCrewMembers()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.MoveCrewMember(ship.Crew[1], ship.Cells[6], ship);
            PlayerCommands.MoveCrewMember(ship.Crew[0], ship.Cells[5], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[6], ship.Crew[1].Cell);
            Assert.AreEqual(ship.Cells[5], ship.Crew[0].Cell);

            PlayerCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 10;
            ship.Crew[0].CurrentHP = 20;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Crew[1].CurrentHP);
            Assert.AreEqual(22, ship.Crew[0].CurrentHP);
        }

        [Test]
        public void TestHealFullHp()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.MoveCrewMember(ship.Crew[1], ship.Cells[6], ship);
            PlayerCommands.MoveCrewMember(ship.Crew[0], ship.Cells[5], ship);
            CrewActionsHandler.TickCrew(ship);
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(ship.Cells[6], ship.Crew[1].Cell);
            Assert.AreEqual(ship.Cells[5], ship.Crew[0].Cell);

            PlayerCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            ship.Crew[1].CurrentHP = 10;
            ship.Crew[0].CurrentHP = 100;
            CrewActionsHandler.TickCrew(ship);
            Assert.AreEqual(12, ship.Crew[1].CurrentHP);
            Assert.AreEqual(100, ship.Crew[0].CurrentHP);
        }

        [Test]
        public void TestHealingOutOfLivingRoom()
        {
            var ship = new TestShip(Alignment.Player);
            Assert.IsTrue(ship.Crew.All(c => c.Action == CrewAction.Idle));
            CrewActionsHandler.TickCrew(ship);

            PlayerCommands.TrySetRoomEnergyConsumption(ship.SpecialRooms[2], 2, ship);
            SpecialRoomBonusCalculator.Recalculate(ship);
            foreach (var crew in ship.Crew)
                crew.CurrentHP = 10;
            CrewActionsHandler.TickCrew(ship);
            Assert.IsTrue(ship.Crew.All(c => c.CurrentHP == 10));
        }
    }
}
