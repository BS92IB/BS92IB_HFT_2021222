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
    public class ShipLogicTests
    {
        private ShipLogic shipLogic;
        private Ship fakeShip;
        private Mock<IShipRepository> mockShipRepository;

        [SetUp]
        public void Init()
        {
            mockShipRepository = new Mock<IShipRepository>();
            mockShipRepository.Setup(x => x.Create(It.IsAny<Ship>()));
            shipLogic = new ShipLogic(mockShipRepository.Object);

            fakeShip = new Ship()
            {
                Name = "HMS Belfast",
                Class = "Town",
                HullType = "LightCruiser",
                Length = 187,
                Beam = 19.3,
                Draft = 6,
                Displacement = 11550,
                MaxSpeedKnots = 32
            };
        }

        [Test]
        public void CreateShipTest_throws_on_invalid_name()
        {
            fakeShip.Name = null;
            var ex = Assert.Throws<ArgumentException>(() => shipLogic.Create(fakeShip));
            Assert.That(ex.Message, Is.EqualTo("Name cannot be Null."));
        }

        [Test]
        public void CreateShipTest_throws_on_invalid_class()
        {
            fakeShip.Class = null;
            var ex = Assert.Throws<ArgumentException>(() => shipLogic.Create(fakeShip));
            Assert.That(ex.Message, Is.EqualTo("Class cannot be Null."));
        }

        [Test]
        public void CreateShipTest_throws_on_invalid_hull_type()
        {
            fakeShip.HullType = null;
            var ex = Assert.Throws<ArgumentException>(() => shipLogic.Create(fakeShip));
            Assert.That(ex.Message, Is.EqualTo("HullType cannot be Null."));
        }

        [Test]
        public void CreateShipTest_throws_on_invalid_size()
        {
            fakeShip.Displacement = -1;
            var ex = Assert.Throws<ArgumentException>(() => shipLogic.Create(fakeShip));
            Assert.That(ex.Message, Is.EqualTo("Displacement and Sizes cannot be <= 0"));
        }

        [Test]
        public void CreateShipTest_throws_on_invalid_speed()
        {
            fakeShip.MaxSpeedKnots = -1;
            var ex = Assert.Throws<ArgumentException>(() => shipLogic.Create(fakeShip));
            Assert.That(ex.Message, Is.EqualTo("MaxSpeed cannot be < 0"));
        }

        [Test]
        public void CreateShipTest_calls_repository_create()
        {
            shipLogic.Create(fakeShip);

            // Verify that shipLogic called the repository with the argument passed in.
            mockShipRepository.Verify(x => x.Create(fakeShip), Times.Once);
        }
    }
}
