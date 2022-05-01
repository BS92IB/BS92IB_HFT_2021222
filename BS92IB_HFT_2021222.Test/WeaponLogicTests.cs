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
    public class WeaponLogicTests
    {
        private WeaponLogic weaponLogic;
        private Weapon fakeWeapon;
        private Mock<IWeaponRepository> mockWeaponRepository;

        [SetUp]
        public void Init()
        {
            mockWeaponRepository = new Mock<IWeaponRepository>();
            mockWeaponRepository.Setup(x => x.Create(It.IsAny<Weapon>()));
            weaponLogic = new WeaponLogic(mockWeaponRepository.Object);

            fakeWeapon = new Weapon()
            {
                Designation = "380mm SK C/34",
                WeaponType = "Naval gun"
            };
        }

        [Test]
        public void CreateWeaponTest_throws_on_invalid_designation()
        {
            fakeWeapon.Designation = null;
            var ex = Assert.Throws<ArgumentException>(() => weaponLogic.Create(fakeWeapon));
            Assert.That(ex.Message, Is.EqualTo("Designation cannot be Null."));
        }

        [Test]
        public void CreateWeaponTest_throws_on_invalid_type()
        {
            fakeWeapon.WeaponType = null;
            var ex = Assert.Throws<ArgumentException>(() => weaponLogic.Create(fakeWeapon));
            Assert.That(ex.Message, Is.EqualTo("WeaponType cannot be Null."));
        }

        [Test]
        public void CreateWeaponTest_calls_repository_create()
        {
            weaponLogic.Create(fakeWeapon);

            // Verify that weaponLogic called the repository with the argument passed in.
            mockWeaponRepository.Verify(x => x.Create(fakeWeapon), Times.Once);
        }
    }
}
