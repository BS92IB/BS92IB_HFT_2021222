using BS92IB_HFT_2021222.Data;
using BS92IB_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Repository
{
    public class ArmamentRepository : IArmamentRepository
    {
        private readonly NavyDbContext db;
        public ArmamentRepository(NavyDbContext db)
        {
            this.db = db;
        }
        public void Create(Armament armament)
        {
            if (armament.Weapon != null && armament.Weapon.Id != default)
            {
                // otherwise it throws an exception trying to persist the weapon again
                // WeaponId is already assigned, that should be enough
                armament.Weapon = null;
            }
            db.Armaments.Add(armament);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var armamentToDelete = Read(id);
            db.Armaments.Remove(armamentToDelete);
            db.SaveChanges();
        }

        public Armament Read(int id)
        {
            return db.Armaments
                .FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Armament> ReadAll()
        {
            return db.Armaments;
        }

        public void Update(Armament armament)
        {
            var oldArmament = Read(armament.Id);
            oldArmament.Name = armament.Name;
            oldArmament.Quantity = armament.Quantity;
            oldArmament.WeaponId = armament.WeaponId;
            oldArmament.ShipId = armament.ShipId;
            db.SaveChanges();
        }
    }
}
