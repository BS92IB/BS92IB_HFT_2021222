using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Data
{
    public class SeedDataBuilder
    {
        // keep track of the Id-s generated for seed data
        int shipId = 1;
        int armamentId = 1;

        // known gun types
        Weapon gun1 = new Weapon { Id = 1, Designation = "Type 3 127mm 50 cal.", WeaponType = "Naval gun" };
        Weapon aa1 = new Weapon { Id = 2, Designation = "Type 93 13mm machine gun", WeaponType = "AA gun" };
        Weapon torp1 = new Weapon { Id = 3, Designation = "610mm torpedo tube", WeaponType = "Torpedo" };

        List<Weapon> weapons = new List<Weapon>();
        List<Armament> armaments = new List<Armament>();

        public SeedDataBuilder()
        {
            weapons.Add(gun1);
            weapons.Add(aa1);
            weapons.Add(torp1);
        }

        /// <summary>
        /// All Armaments created by the data builder while building ships.
        /// </summary>
        public IEnumerable<Armament> Armaments => armaments;

        /// <summary>
        /// All Weapon definitons known by the data builder.
        /// </summary>
        public IEnumerable<Weapon> Weapons => weapons;

        /// <summary>
        /// Builds seed data for an Akatsuki-class destroyer with default armaments.
        /// </summary>
        /// <param name="fleetId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Ship BuildAkatsukiClass(int fleetId, string name)
        {
            Ship ship = new Ship
            {
                Id = shipId++,
                Name = name,
                FleetId = fleetId,
                Class = "Akatsuki",
                HullType = "Destroyer",
                Displacement = 2080,
                Length = 115,
                Beam = 10.4,
                Draft = 3.2,
                MaxSpeedKnots = 38
            };

            armaments.Add(new Armament { Id = armamentId++, Name = "Turret A", Quantity = 2, WeaponId = gun1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "Turret B", Quantity = 2, WeaponId = gun1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "Turret C", Quantity = 2, WeaponId = gun1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "AA mount 1", Quantity = 1, WeaponId = aa1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "AA mount 2", Quantity = 1, WeaponId = aa1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "Torpedo launcher 1", Quantity = 3, WeaponId = torp1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "Torpedo launcher 2", Quantity = 3, WeaponId = torp1.Id, ShipId = ship.Id });
            armaments.Add(new Armament { Id = armamentId++, Name = "Torpedo launcher 3", Quantity = 3, WeaponId = torp1.Id, ShipId = ship.Id });

            return ship;
        }
    }
}
