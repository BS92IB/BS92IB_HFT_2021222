using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Test
{
    [TestFixture]
    public class FleetLogicTests
    {
        private FleetLogic fleetLogic;
        private Mock<IFleetRepository> mockFleetRepository;

        [SetUp]
        public void Init()
        {
            mockFleetRepository = new Mock<IFleetRepository>();

            var fleet1 = new Fleet() { Id = 1, Name = "Fleet1", Ships = new List<Ship>() };
            var fleet2 = new Fleet() { Id = 2, Name = "Fleet2", Ships = new List<Ship>() };

            var ship1a = new Ship() { Id = 1, Name = "Ship1a", Armaments = new List<Armament>(), Fleet = fleet1, FleetId = fleet1.Id, MaxSpeedKnots = 20, HullType = "Destroyer", Displacement = 3300 };
            var ship1b = new Ship() { Id = 2, Name = "Ship1b", Armaments = new List<Armament>(), Fleet = fleet1, FleetId = fleet1.Id, MaxSpeedKnots = 15, HullType = "LightCruiser", Displacement = 7500 };
            var ship2a = new Ship() { Id = 3, Name = "Ship2a", Armaments = new List<Armament>(), Fleet = fleet2, FleetId = fleet2.Id, MaxSpeedKnots = 25, HullType = "Destroyer", Displacement = 2200 };
            var ship2b = new Ship() { Id = 4, Name = "Ship2b", Armaments = new List<Armament>(), Fleet = fleet2, FleetId = fleet2.Id, MaxSpeedKnots = 10, HullType = "Carrier", Displacement = 36000 };

            fleet1.Ships.Add(ship1a);
            fleet1.Ships.Add(ship1b);
            fleet2.Ships.Add(ship2a);
            fleet2.Ships.Add(ship2b);

            var aaGun = new Weapon() { Id = 1, WeaponType = "AA gun" };
            var otherGun = new Weapon() { Id = 2, WeaponType = "Naval gun" };

            var ship1a_armament1 = new Armament() { Id = 1, Ship = ship1a, ShipId = ship1a.Id, Quantity = 2, Weapon = aaGun, WeaponId = aaGun.Id };
            var ship1b_armament1 = new Armament() { Id = 2, Ship = ship1b, ShipId = ship1b.Id, Quantity = 8, Weapon = aaGun, WeaponId = aaGun.Id };
            var ship1b_armament2 = new Armament() { Id = 3, Ship = ship1b, ShipId = ship1b.Id, Quantity = 6, Weapon = otherGun, WeaponId = otherGun.Id };
            var ship2a_armament1 = new Armament() { Id = 4, Ship = ship2a, ShipId = ship2a.Id, Quantity = 4, Weapon = aaGun, WeaponId = aaGun.Id };
            var ship2b_armament1 = new Armament() { Id = 5, Ship = ship2b, ShipId = ship2b.Id, Quantity = 4, Weapon = aaGun, WeaponId = aaGun.Id };

            ship1a.Armaments.Add(ship1a_armament1);
            ship1b.Armaments.Add(ship1b_armament1);
            ship1b.Armaments.Add(ship1b_armament2);
            ship2a.Armaments.Add(ship2a_armament1);
            ship2b.Armaments.Add(ship2b_armament1);

            var fleets = new List<Fleet>() { fleet1, fleet2 }
                .AsQueryable();

            mockFleetRepository.Setup(x => x.ReadAll()).Returns(fleets);

            // Some methods may need this as well
            mockFleetRepository.Setup(x => x.Read(It.IsAny<int>())).Returns<int>(id => fleets.Single(f => f.Id == id));

            mockFleetRepository.Setup(x => x.Create(It.IsAny<Fleet>()));

            fleetLogic = new FleetLogic(mockFleetRepository.Object);
        }

        [Test]
        public void FastestFleetTest()
        {
            var result = fleetLogic.FastestFleet();
            Assert.That(result.Name, Is.EqualTo("Fleet1"));
        }

        [Test]
        public void FleetsWithoutCarrierTest()
        {
            var result = fleetLogic.FleetsWithoutCarrier();
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.Single().Ships.Select(s => s.HullType), Is.All.Not.EqualTo("Carrier"));
        }

        [Test]
        public void MostArmedFleetTest()
        {
            var result = fleetLogic.MostArmedFleet();
            Assert.That(result.Name, Is.EqualTo("Fleet1"));
            Assert.That(result.Ships.SelectMany(s => s.Armaments).Sum(a => a.Quantity), Is.EqualTo(16));
        }

        [Test]
        public void TotalAaGunsTest()
        {
            var fleet = fleetLogic.Read(1);
            var result = fleetLogic.TotalAaGuns(fleet);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void TotalDisplacementTest()
        {
            var fleet = fleetLogic.Read(1);
            var result = fleetLogic.TotalDisplacement(fleet);
            Assert.That(result, Is.EqualTo(10800).Within(0.001));
        }

        [Test]
        public void CreateFleetTest_throws_on_invalid_name()
        {
            var fakeFleet = new Fleet() { Name = null };
            Assert.Throws<ArgumentException>(() => fleetLogic.Create(fakeFleet));
        }

        [Test]
        public void CreateFleetTest_calls_repository_create()
        {
            var fakeFleet = new Fleet() { Name = "Test" };
            fleetLogic.Create(fakeFleet);
            mockFleetRepository.Verify(x => x.Create(fakeFleet), Times.Once);
        }
    }
}
