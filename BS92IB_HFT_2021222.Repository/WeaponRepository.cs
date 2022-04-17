using BS92IB_HFT_2021222.Data;
using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly NavyDbContext db;
        public WeaponRepository(NavyDbContext db)
        {
            this.db = db;
        }
        public void Create(Weapon weapon)
        {
            db.Weapons.Add(weapon);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var weaponToDelete = Read(id);
            db.Weapons.Remove(weaponToDelete);
            db.SaveChanges();
        }

        public Weapon Read(int id)
        {
            return db.Weapons
                .FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Weapon> ReadAll()
        {
            return db.Weapons;
        }

        public void Update(Weapon weapon)
        {
            var oldWeapon = Read(weapon.Id);
            oldWeapon.Designation = weapon.Designation;
            oldWeapon.WeaponType = weapon.WeaponType;
            db.SaveChanges();
        }
    }
}
