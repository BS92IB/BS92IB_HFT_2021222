using BS92IB_HFT_2021222.Models;
using BS92IB_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Logic
{
    public class WeaponLogic : IWeaponLogic
    {
        IWeaponRepository weaponRepo;
        public WeaponLogic(IWeaponRepository weaponRepo)
        {
            this.weaponRepo = weaponRepo;
        }
        public void Create(Weapon weapon)
        {
            if (string.IsNullOrEmpty(weapon.Designation))
            {
                throw new ArgumentException("Designation cannot be Null.");
            }
            if (string.IsNullOrEmpty(weapon.WeaponType))
            {
                throw new ArgumentException("WeaponType cannot be Null.");
            }
            weaponRepo.Create(weapon);
        }

        public void Delete(int id)
        {
            weaponRepo.Delete(id);
        }

        public Weapon Read(int id)
        {
            return weaponRepo.Read(id);
        }

        public IEnumerable<Weapon> ReadAll()
        {
            return weaponRepo.ReadAll();
        }

        public void Update(Weapon weapon)
        {
            weaponRepo.Update(weapon);
        }
    }
}
