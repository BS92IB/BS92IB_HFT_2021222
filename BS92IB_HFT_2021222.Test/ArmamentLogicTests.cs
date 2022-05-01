using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;

namespace BS92IB_HFT_2021222.Test
{
    [TestFixture]
    public class ArmamentLogicTests
    {
        private ArmamentLogic armamentLogic;
        private Armament fakeArmament;
        private Mock<IArmamentRepository> mockArmamentRepository;

        [SetUp]
        public void Init()
        {
            mockArmamentRepository = new Mock<IArmamentRepository>();
            mockArmamentRepository.Setup(x => x.Create(It.IsAny<Armament>()));
            armamentLogic = new ArmamentLogic(mockArmamentRepository.Object);

            fakeArmament = new Armament()
            {
                Name = "Quadruple Turret A",
                Quantity = 4
            };
        }

        [Test]
        public void CreateArmamentTest_throws_on_invalid_quantity()
        {
            fakeArmament.Quantity = -1;
            var ex = Assert.Throws<ArgumentException>(() => armamentLogic.Create(fakeArmament));
            Assert.That(ex.Message, Is.EqualTo("Quantity cannot be <= 0"));
        }

        [Test]
        public void CreateArmamentTest_throws_on_invalid_name()
        {
            fakeArmament.Name = null;
            var ex = Assert.Throws<ArgumentException>(() => armamentLogic.Create(fakeArmament));
            Assert.That(ex.Message, Is.EqualTo("Name cannot be Null."));
        }

        [Test]
        public void CreateArmamentTest_calls_repository_create()
        {
            armamentLogic.Create(fakeArmament);

            // Verify that armamentLogic called the repository with the argument passed in.
            mockArmamentRepository.Verify(x => x.Create(fakeArmament), Times.Once);
        }
    }
}
